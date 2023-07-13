using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using LaRoy.ORM.Utils;
using Npgsql;
using Npgsql.Internal;
using MySql.Data.MySqlClient;
using LaRoy.Mapper.BulkOperations.Utils;

namespace LaRoy.ORM.BulkOperations
{
    public static partial class BulkOperations
    {
        public static void BulkDelete<T>(this DbConnection connection, IEnumerable<T> data)
        {
            connection.Open();
            string keyFieldName = data.First().GetKeyField().Name;
            Type keyFieldType = data.First().GetKeyField().PropertyType;
            string tableName = typeof(T).Name;
            string tempTableName = string.Empty;
            DataTable tempTable = new DataTable();
            tempTable.Columns.Add(keyFieldName, keyFieldType);
            foreach (var item in data)
            {
                var id = item.GetKeyField().GetValue(item);
                tempTable.Rows.Add(id);
            }
            try
            {
                if (connection is SqlConnection sqlConnection)
                {
                    tempTableName = "#TempIDs";
                    sqlConnection.CreateTemporaryTable<T>(tempTableName, onlyKeyField: true);
                    using SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnection);
                    bulkCopy.DestinationTableName = tempTableName;
                    bulkCopy.WriteToServer(tempTable);
                }
                else if (connection is NpgsqlConnection npgsqlConnection)
                {
                    tempTableName = "TempIDs";
                    npgsqlConnection.CreateTemporaryTable<T>(tempTableName, onlyKeyField: true);
                    using var binaryImporter = npgsqlConnection.BeginBinaryImport($"COPY {tempTableName} FROM STDIN (FORMAT BINARY)");
                    foreach (DataRow row in tempTable.Rows)
                    {
                        binaryImporter.StartRow();
                        var value = row[tempTable.Columns[0].ColumnName];
                        var type = tempTable.Columns[0].DataType.GetNpgsqlDbType();
                        binaryImporter.Write(value, type);
                    }
                    binaryImporter.Complete();
                }
                else if (connection is MySqlConnection mySqlConnection)
                {
                    tempTableName = "TempIDs";
                    mySqlConnection.CreateTemporaryTable<T>(tempTableName, onlyKeyField: true);
                    using MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
                    MySqlCommandBuilder commandBuilder = new MySqlCommandBuilder(dataAdapter);
                    dataAdapter.SelectCommand = mySqlConnection.CreateCommand();
                    dataAdapter.SelectCommand.CommandText = $"SELECT * FROM {tempTableName}";
                    dataAdapter.InsertCommand = commandBuilder.GetInsertCommand();
                    dataAdapter.Update(tempTable);
                }
                string deleteQuery = $"DELETE FROM {tableName} WHERE [{keyFieldName}] IN (SELECT [{keyFieldName}] FROM {tempTableName})";

                using DbCommand deleteCommand = connection.GetSpecificCommandType(deleteQuery);
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

        public static void BulkDelete<T>(this DbConnection connection, T[] data)
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
