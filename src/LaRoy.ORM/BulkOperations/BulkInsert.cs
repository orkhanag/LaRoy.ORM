using LaRoy.Mapper.BulkOperations.Utils;
using LaRoy.ORM.Queries;
using LaRoy.ORM.Utils;
using MySql.Data.MySqlClient;
using Npgsql;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace LaRoy.ORM.BulkOperations
{
    public static partial class BulkOperations
    {
        public static int BulkInsert<T>(this IDbConnection connection, IEnumerable<T> data)
        {
            try
            {
                var tableName = typeof(T).Name;
                var dataTable = data.ToDataTable();

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                if (connection is SqlConnection sqlConnection)
                    sqlConnection.SqlBulkInsert(tableName, dataTable);

                else if (connection is NpgsqlConnection npgSqlConnection)
                    npgSqlConnection.NpgSqlBulkInsert(tableName, dataTable);

                else if (connection is MySqlConnection mySqlConnection)
                    mySqlConnection.MySqlBulkInsert(tableName, dataTable);

                return dataTable.Rows.Count;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public static int BulkInsert<T>(this LaRoyDbContext context, IEnumerable<T> data)
        {
            var connection = context.GetConnection();
            return connection.BulkInsert(data);
        }

        public static int BulkInsert<T>(this IDbConnection connection, T[] data)
        {
            var dataAsEnumerable = data.AsEnumerable();
            return BulkInsert<T>(connection, dataAsEnumerable);
        }

        public static int BulkInsert<T>(this LaRoyDbContext context, T[] data)
        {
            var connection = context.GetConnection();
            return connection.BulkInsert(data);
        }
    }
}
