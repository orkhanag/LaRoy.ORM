using System.Data.SqlClient;
using LaRoy.ORM.Utils;
using System.Data.Common;
using LaRoy.Mapper.BulkOperations.Utils;
using Npgsql;
using System.Data;
using MySql.Data.MySqlClient;

namespace LaRoy.ORM.BulkOperations
{
    public static partial class BulkOperations
    {
        public static void BulkUpdate<T>(this IDbConnection connection, IEnumerable<T> data)
        {
            try
            {
                var tableName = typeof(T).Name;
                var dataTable = data.ToDataTable();

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                string tempTableName = connection is SqlConnection ? "#TempTable" : "TempTable";
                connection.CreateTemporaryTable<T>(tempTableName);

                if (connection is SqlConnection sqlConnection)
                    sqlConnection.SqlBulkInsert(tempTableName, dataTable);

                else if (connection is NpgsqlConnection npgSqlConnection)
                    npgSqlConnection.NpgSqlBulkInsert(tempTableName, dataTable);

                else if (connection is MySqlConnection mySqlConnection)
                    mySqlConnection.MySqlBulkInsert(tempTableName, dataTable);

                connection.UpdateDataFromTempTable<T>(tableName, tempTableName);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally { connection.Close(); }
        }

        public static void BulkUpdate<T>(this LaRoyDbContext context, IEnumerable<T> data)
        {
            var connection = context.GetConnection();
            connection.BulkUpdate(data);
        }

        public static void BulkUpdate<T>(this IDbConnection connection, T[] data)
        {
            var dataAsEnumarable = data.AsEnumerable();
            connection.BulkUpdate(dataAsEnumarable);
        }

        public static void BulkUpdate<T>(this LaRoyDbContext context, T[] data)
        {
            var connection = context.GetConnection();
            connection.BulkUpdate(data);
        }
    }
}

