using MySql.Data.MySqlClient;
using Npgsql;
using NpgsqlTypes;
using System.Data.Common;
using System.Data.SqlClient;
using System.Net;

namespace LaRoy.ORM.Utils
{
    public static class DatabaseManupulations
    {
        private static string GenerateCreateTableQuery<T>(string tableName, DbConnection connection)
        {
            // Get the properties of the object type
            var properties = typeof(T).GetProperties();

            // Generate the column definitions
            List<string> columnDefinitions = new List<string>();
            foreach (var property in properties)
            {
                string columnName = property.Name;
                string columnType = property.PropertyType.GetDbTypeAsString(connection);

                columnDefinitions.Add($"{columnName} {columnType}");
            }

            // Combine the column definitions into the CREATE TABLE statement
            string columnDefinitionsStr = string.Join(", ", columnDefinitions);
            var isNpg = connection is NpgsqlConnection;
            var isMySql = connection is MySqlConnection;
            var npgSpecificSyntax = isNpg ? "TEMP" : string.Empty;
            var mySqlSpecificSyntax = isMySql ? "TEMPORARY" : string.Empty;
            if (isNpg || isMySql)
                tableName = tableName.Trim('#');
            string createTableQuery = $"CREATE {(isNpg ? npgSpecificSyntax : mySqlSpecificSyntax)} TABLE {tableName} ({columnDefinitionsStr})";

            return createTableQuery;
        }

        public static void CreateTemporaryTable<T>(this DbConnection connection, string tableName)
        {
            using (DbCommand command = connection.GetSpecificCommandType())
            {
                command.CommandText = GenerateCreateTableQuery<T>(tableName, connection);
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

        public static DbCommand GetSpecificCommandType(this DbConnection connection)
        {
            return connection switch
            {
                SqlConnection sqlConnection => new SqlCommand("", sqlConnection),
                NpgsqlConnection npgSqlConnection => new NpgsqlCommand("", npgSqlConnection),
                MySqlConnection mySqlConnection => new MySqlCommand("", mySqlConnection),
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
