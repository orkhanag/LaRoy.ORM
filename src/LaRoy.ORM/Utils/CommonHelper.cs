using MySql.Data.MySqlClient;
using Npgsql;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Reflection;

namespace LaRoy.ORM.Utils
{
    public static class CommonHelper
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> data)
        {
            var tableName = typeof(T).Name;
            DataTable dataTable = new DataTable(tableName);
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
                dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            if (data is not null)
            {
                foreach (var item in data)
                {
                    DataRow row = dataTable.NewRow();
                    foreach (var property in properties)
                        row[property.Name] = property.GetValue(item) ?? DBNull.Value;
                    dataTable.Rows.Add(row);
                }
            }
            return dataTable;
        }

        public static PropertyInfo GetKeyField<T>()
        {
            var properties = typeof(T).GetProperties();
            foreach (var prop in properties)
            {
                var attributes = prop.GetCustomAttributes(false);
                if (attributes.Where(x => x is KeyAttribute).Any())
                    return prop;
            }
            throw new NotSupportedException($"Type {typeof(T).Name} doesn't have 'Key' field!");
        }

        public static string GetSpecificUpdateCommandText(this IDbCommand command, string tableName, string columnValues, string tempTableName, string keyFieldName)
        {
            return command switch
            {
                SqlCommand => $@"UPDATE {tableName} SET
                                                {columnValues}
                                                FROM {tempTableName} AS tmp
                                                WHERE {tableName}.{keyFieldName} = tmp.{keyFieldName}",
                NpgsqlCommand => $@"UPDATE {tableName} SET
                                                {columnValues}
                                                FROM {tempTableName} AS tmp
                                                WHERE {tableName}.{keyFieldName} = tmp.{keyFieldName}",
                MySqlCommand => $@"UPDATE {tableName} dest, {tempTableName} src SET
                                                {columnValues.Trim(',')}
                                              WHERE dest.{keyFieldName}=src.{keyFieldName}"

            };
        }

        public static string GenerateCreateTableQuery<T>(string tableName, IDbConnection connection, bool onlyKeyField = false)
        {
            var properties = typeof(T).GetProperties();
            string columnDefinitionsStr = string.Empty;
            if (onlyKeyField)
            {
                var keyField = CommonHelper.GetKeyField<T>();
                columnDefinitionsStr = $"{keyField.Name} {keyField.PropertyType.GetDbTypeAsString(connection)}";
            }
            else
            {
                List<string> columnDefinitions = new();
                foreach (var property in properties)
                    columnDefinitions.Add($"{property.Name} {property.PropertyType.GetDbTypeAsString(connection)}");

                columnDefinitionsStr = string.Join(", ", columnDefinitions);
            }
            string altSyntax = string.Empty;
            if (connection is not SqlConnection)
                altSyntax = connection is NpgsqlConnection ? "TEMP" : "TEMPORARY";
            string createTableQuery = $"CREATE {altSyntax} TABLE {tableName} ({columnDefinitionsStr})";
            return createTableQuery;
        }

        public static bool IsAnonymousType(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return Attribute.IsDefined(type, typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), false)
                   && type.IsGenericType
                   && type.Name.Contains("AnonymousType")
                   && (type.Name.StartsWith("<>") || type.Name.StartsWith("VB$"))
                   && (type.Attributes & TypeAttributes.NotPublic) == TypeAttributes.NotPublic;
        }

        public static void AddParams(IDbCommand command, object param)
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>();

            if (param.GetType().IsAnonymousType())
            {
                var properties = param.GetType().GetProperties();

                foreach (var property in properties)
                {
                    var paramName = "@" + property.Name;
                    var propertyValue = property.GetValue(param);
                    parameters[paramName] = propertyValue;
                }
            }
            else if (param is IDictionary<string, object> dictParam)
                parameters = dictParam;
            else
                throw new ArgumentException("Invalid parameter type. Only anonymous types or IDictionary<string, object> are supported.");

            foreach (var parameter in parameters)
            {
                var paramName = parameter.Key;
                var dbParameter = command.CreateParameter();
                dbParameter.ParameterName = paramName;
                dbParameter.Value = parameter.Value;
                command.Parameters.Add(dbParameter);
            }
        }

        public static dynamic ToExpandoObject(this IDataReader reader)
        {
            dynamic result = new ExpandoObject();
            var dict = result as IDictionary<string, object>;

            for (int i = 0; i < reader.FieldCount; i++)
            {
                var columnName = reader.GetName(i);
                var columnValue = reader.GetValue(i);
                dict[columnName] = columnValue;
            }

            return result;
        }

        public static T ToStrongType<T>(this IDataReader reader)
        {
            var result = Activator.CreateInstance<T>();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var columnName = property.Name;
                var columnValue = reader[columnName];

                if (columnValue != DBNull.Value)
                {
                    property.SetValue(result, columnValue);
                }
            }
            return (T)result;
        }
    }
}
