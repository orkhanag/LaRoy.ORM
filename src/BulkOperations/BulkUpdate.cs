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
            var tableName = typeof(T).Name;
            var dataTable = data.ToDataTable();
            if (connection.State != ConnectionState.Open)
                connection.Open();
            if (connection is SqlConnection sqlConnection)
            {
                using IDbCommand command = sqlConnection.GetSpecificCommandType();
                try
                {
                    sqlConnection.CreateTemporaryTable<T>("#TmpTable");
                    using (SqlBulkCopy bulkcopy = new(sqlConnection))
                    {
                        bulkcopy.BulkCopyTimeout = 0;
                        bulkcopy.DestinationTableName = "#TmpTable";
                        bulkcopy.WriteToServer(dataTable);
                        bulkcopy.Close();
                    }
                    var properties = typeof(T).GetProperties();
                    var columnValues = string.Empty;
                    foreach (var prop in properties)
                        columnValues += $"{prop.Name} = tmp.{prop.Name},";
                    var keyFieldName = DataManupulations.GetKeyField<T>().Name;
                    command.CommandTimeout = 300;
                    command.CommandText = $@"UPDATE {tableName} SET
                                                {columnValues.Trim(',')}
                                                FROM #TmpTable AS tmp
                                                WHERE {tableName}.{keyFieldName} = tmp.{keyFieldName}";
                    command.ExecuteNonQuery();
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
            else if (connection is NpgsqlConnection npgSqlConnection)
            {
                using IDbCommand command = npgSqlConnection.GetSpecificCommandType();
                try
                {
                    npgSqlConnection.CreateTemporaryTable<T>("TmpTable");
                    using (var binaryImporter = npgSqlConnection.BeginBinaryImport($"COPY TmpTable FROM STDIN (FORMAT BINARY)"))
                    {
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

                    var properties = typeof(T).GetProperties();
                    var columnValues = string.Empty;
                    foreach (var prop in properties)
                        columnValues += $"{prop.Name} = tmp.{prop.Name},";
                    var keyFieldName = DataManupulations.GetKeyField<T>().Name;
                    command.CommandTimeout = 300;
                    command.CommandText = $@"UPDATE {tableName} SET
                                                {columnValues.Trim(',')}
                                                FROM TmpTable AS tmp
                                                WHERE {tableName}.{keyFieldName} = tmp.{keyFieldName}";
                    command.ExecuteNonQuery();
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
            else if (connection is MySqlConnection mySqlConnection)
            {
                using IDbCommand command = mySqlConnection.GetSpecificCommandType();
                try
                {
                    mySqlConnection.CreateTemporaryTable<T>("TmpTable");
                    using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter())
                    {
                        MySqlCommandBuilder commandBuilder = new MySqlCommandBuilder(dataAdapter);
                        dataAdapter.SelectCommand = mySqlConnection.CreateCommand();
                        dataAdapter.SelectCommand.CommandText = $"SELECT * FROM TmpTable";
                        dataAdapter.InsertCommand = commandBuilder.GetInsertCommand();
                        dataAdapter.Update(dataTable);
                    }
                    var properties = typeof(T).GetProperties();
                    var columnValues = string.Empty;
                    foreach (var prop in properties)
                        columnValues += $"dest.{prop.Name} = src.{prop.Name},";
                    var keyFieldName = DataManupulations.GetKeyField<T>().Name;
                    command.CommandTimeout = 300;
                    command.CommandText = $@"UPDATE {tableName} dest, TmpTable src SET
                                                {columnValues.Trim(',')}
                                              WHERE dest.{keyFieldName}=src.{keyFieldName}";
                    command.ExecuteNonQuery();
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

