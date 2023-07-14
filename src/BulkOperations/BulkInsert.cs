using LaRoy.Mapper.BulkOperations.Utils;
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

                var cc = connection.GetSpecificConnectionType();

                if (connection is SqlConnection sqlConnection)
                {
                    using SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnection);
                    bulkCopy.DestinationTableName = tableName;
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                        bulkCopy.ColumnMappings.Add(i, i);
                    bulkCopy.WriteToServer(dataTable);
                }
                else if (connection is NpgsqlConnection npgSqlConnection)
                {
                    using var binaryImporter = npgSqlConnection.BeginBinaryImport($"COPY {tableName} FROM STDIN (FORMAT BINARY)");
                    foreach (DataRow row in dataTable.Rows)
                    {
                        binaryImporter.StartRow();
                        foreach (DataColumn column in dataTable.Columns)
                        {
                            var value = row[column.ColumnName];
                            var type = column.DataType.GetNpgsqlDbType();
                            binaryImporter.Write(value, type);
                        }
                    }
                    binaryImporter.Complete();
                }
                else if (connection is MySqlConnection mySqlConnection)
                {
                    using MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
                    MySqlCommandBuilder commandBuilder = new MySqlCommandBuilder(dataAdapter);
                    dataAdapter.SelectCommand = mySqlConnection.CreateCommand();
                    dataAdapter.SelectCommand.CommandText = $"SELECT * FROM {tableName}";
                    dataAdapter.InsertCommand = commandBuilder.GetInsertCommand();
                    dataAdapter.Update(dataTable);
                }
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
