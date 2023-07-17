using MySql.Data.MySqlClient;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace LaRoy.ORM.Utils
{
    public static class DbHelper
    {
        public static string GetDbTypeAsString(this Type dataType, IDbConnection connection)
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
