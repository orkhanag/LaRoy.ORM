using MySql.Data.MySqlClient;
using MySql.Data.MySqlClient.X.XDevAPI.Common;
using Npgsql;
using NpgsqlTypes;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Data.SqlClient;
using System.Net;

namespace LaRoy.ORM.Utils
{
    public static class DatabaseManupulations
    {
        private static string GenerateCreateTableQuery<T>(string tableName, DbConnection connection, bool onlyKeyField = false)
        {
            var properties = typeof(T).GetProperties();
            string columnDefinitionsStr = string.Empty;
            if (onlyKeyField)
                foreach (var property in properties)
                    if (property.GetCustomAttributes(false).Where(x => x is KeyAttribute).Any())
                        columnDefinitionsStr = $"{property.Name} {property.PropertyType.GetDbTypeAsString(connection)}";
                    else
                        throw new NotSupportedException($"Type {typeof(T).Name} doesn't have 'Key' field!");
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

        public static void CreateTemporaryTable<T>(this DbConnection connection, string tableName, bool onlyKeyField = false)
        {
            using (DbCommand command = connection.GetSpecificCommandType())
            {
                command.CommandText = GenerateCreateTableQuery<T>(tableName, connection, onlyKeyField);
                command.ExecuteNonQuery();
            }
        }

        public static DbConnection GetSpecificConnectionType(this DbConnection connection)
        {
            return connection switch
            {
                SqlConnection sqlConnection => sqlConnection,
                NpgsqlConnection npgSqlConnection => npgSqlConnection,
                MySqlConnection mySqlConnection => mySqlConnection,
                _ => throw new NotSupportedException($"This connection type({connection.GetType().Name}) is not supported!")
            };
        }

        public static DbCommand GetSpecificCommandType(this DbConnection connection, string commandText = "")
        {
            return connection switch
            {
                SqlConnection sqlConnection => new SqlCommand(commandText, sqlConnection),
                NpgsqlConnection npgSqlConnection => new NpgsqlCommand(commandText, npgSqlConnection),
                MySqlConnection mySqlConnection => new MySqlCommand(commandText, mySqlConnection),
                _ => throw new NotSupportedException($"This connection type({connection.GetType().Name}) is not supported!")
            };
        }

        public static string GetDbTypeAsString(this Type dataType, DbConnection connection)
        {
            return connection switch
            {
                SqlConnection => dataType.GetSqlTypeString(),
                NpgsqlConnection => dataType.GetNpgsqlTypeString(),
                MySqlConnection => dataType.GetMySqlTypeString(),
                _ => throw new NotSupportedException($"This connection type({connection.GetType().Name}) is not supported!")
            };
        }

        public static NpgsqlDbType GetNpgsqlDbType(this Type dataType)
        {
            if (dataType == typeof(string))
                return NpgsqlDbType.Text;
            if (dataType == typeof(int))
                return NpgsqlDbType.Integer;
            if (dataType == typeof(double))
                return NpgsqlDbType.Double;
            if (dataType == typeof(decimal))
                return NpgsqlDbType.Numeric;
            if (dataType == typeof(DateTime))
                return NpgsqlDbType.Date;
            if (dataType == typeof(bool))
                return NpgsqlDbType.Boolean;
            if (dataType == typeof(float))
                return NpgsqlDbType.Real;
            if (dataType == typeof(short))
                return NpgsqlDbType.Smallint;
            if (dataType == typeof(long))
                return NpgsqlDbType.Bigint;
            if (dataType == typeof(Guid))
                return NpgsqlDbType.Uuid;
            if (dataType == typeof(TimeSpan))
                return NpgsqlDbType.Interval;
            if (dataType == typeof(IPAddress))
                return NpgsqlDbType.Inet;
            if (dataType == typeof(char))
                return NpgsqlDbType.InternalChar;
            // Add additional type mappings as needed

            throw new NotSupportedException($"NpgsqlDbType mapping not found for type '{dataType.Name}'.");
        }

        public static string GetNpgsqlTypeString(this Type dataType)
        {
            if (dataType == typeof(long))
                return "bigint";
            if (dataType == typeof(bool))
                return "boolean";
            if (dataType == typeof(DateTime) || dataType == typeof(DateTime?))
                return "date";
            if (dataType == typeof(double))
                return "double precision";
            if (dataType == typeof(int))
                return "integer";
            if (dataType == typeof(decimal))
                return "numeric";
            if (dataType == typeof(string))
                return "text";
            // Add additional type mappings as needed

            throw new NotSupportedException($"Column type mapping not found for type '{dataType.Name}'.");
        }

        public static string GetSqlTypeString(this Type dataType)
        {
            if (dataType == typeof(long))
                return "bigint";
            if (dataType == typeof(short))
                return "smallint";
            if (dataType == typeof(bool))
                return "bit";
            if (dataType == typeof(DateTime?) || dataType == typeof(DateTime))
                return "datetime";
            if (dataType == typeof(double))
                return "float";
            if (dataType == typeof(int))
                return "int";
            if (dataType == typeof(decimal))
                return "decimal";
            if (dataType == typeof(string))
                return "nvarchar(MAX)";
            // Add additional type mappings as needed

            throw new NotSupportedException($"Column type mapping not found for type '{dataType.Name}'.");
        }

        public static string GetMySqlTypeString(this Type dataType)
        {
            if (dataType == typeof(long))
                return "bigint";
            if (dataType == typeof(bool))
                return "bit";
            if (dataType == typeof(DateTime) || dataType == typeof(DateTime?))
                return "date";
            if (dataType == typeof(double))
                return "double";
            if (dataType == typeof(int))
                return "int";
            if (dataType == typeof(decimal))
                return "decimal";
            if (dataType == typeof(string))
                return "text";
            // Add additional type mappings as needed

            throw new NotSupportedException($"Column type mapping not found for type '{dataType.Name}'.");
        }
    }
}
