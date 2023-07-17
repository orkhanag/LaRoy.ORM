using System.Data.SqlClient;
using System.Data;
using LaRoy.ORM.Utils;
using Npgsql;
using MySql.Data.MySqlClient;
using LaRoy.Mapper.BulkOperations.Utils;

namespace LaRoy.ORM.BulkOperations
{
    public static partial class BulkOperations
    {
        public static void BulkDelete<T>(this IDbConnection connection, IEnumerable<T> data)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                string keyFieldName = CommonHelper.GetKeyField<T>().Name;
                Type keyFieldType = CommonHelper.GetKeyField<T>().PropertyType;

                string tableName = typeof(T).Name;
                DataTable tempTable = new DataTable();
                tempTable.Columns.Add(keyFieldName, keyFieldType);
                foreach (var item in data)
                {
                    var id = CommonHelper.GetKeyField<T>().GetValue(item);
                    tempTable.Rows.Add(id);
                }

                string tempTableName = connection is SqlConnection ? "#TempIDs" : "TempIDs";
                connection.CreateTemporaryTable<T>(tempTableName, onlyKeyField: true);

                if (connection is SqlConnection sqlConnection)
                    sqlConnection.SqlBulkInsert(tempTableName, tempTable);

                else if (connection is NpgsqlConnection npgsqlConnection)
                    npgsqlConnection.NpgSqlBulkInsert(tempTableName, tempTable);

                else if (connection is MySqlConnection mySqlConnection)
                    mySqlConnection.MySqlBulkInsert(tempTableName, tempTable);

                string deleteQuery = $"DELETE FROM {tableName} WHERE {keyFieldName} IN (SELECT {keyFieldName} FROM {tempTableName})";

                using IDbCommand deleteCommand = connection.CreateCommand();
                deleteCommand.CommandText = deleteQuery;
                deleteCommand.ExecuteNonQuery();
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

        public static void BulkDelete<T>(this LaRoyDbContext context, IEnumerable<T> data)
        {
            var connection = context.GetConnection();
            connection.BulkDelete(data);
        }

        public static void BulkDelete<T>(this IDbConnection connection, T[] data)
        {
            var dataAsEnumerable = data.AsEnumerable();
            connection.BulkDelete(dataAsEnumerable);
        }

        public static void BulkDelete<T>(this LaRoyDbContext context, T[] data)
        {
            var connection = context.GetConnection();
            connection.BulkDelete(data);
        }
    }
}
