using System.Data.SqlClient;
using LaRoy.ORM.Utils;
using System.Data.Common;
using LaRoy.Mapper.BulkOperations.Utils;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LaRoy.ORM.BulkOperations
{
    public static partial class BulkOperations
    {
        public static void BulkUpdate<T>(this DbConnection connection, IEnumerable<T> data)
        {
            var tableName = typeof(T).Name;
            var dataTable = data.ToDataTable();

            if (connection is SqlConnection sqlConnection)
            {
                using (SqlCommand command = new SqlCommand("", sqlConnection))
                {
                    try
                    {
                        DatabaseManupulations.CreateTemporaryTable<T>(sqlConnection, "#TmpTable");

                        //Bulk insert into temp table
                        using (SqlBulkCopy bulkcopy = new SqlBulkCopy(sqlConnection))
                        {
                            bulkcopy.BulkCopyTimeout = 0;
                            bulkcopy.DestinationTableName = "#TmpTable";
                            bulkcopy.WriteToServer(dataTable);
                            bulkcopy.Close();
                        }

                        var properties = typeof(T).GetProperties();
                        var columnValues = string.Empty;

                        foreach (var prop in properties)
                        {
                            columnValues += $"{prop.Name} = tmp.{prop.Name},";
                        }

                        var keyFieldName = data.First().GetKeyFieldName();

                        // Updating destination table, and dropping temp table
                        command.CommandTimeout = 300;
                        command.CommandText = $@"UPDATE {tableName} SET
                                                {columnValues.Trim(',')}
                                                FROM #TmpTable AS tmp
                                                WHERE {tableName}.{keyFieldName} = tmp.{keyFieldName}";
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public static void BulkUpdate<T>(this LaRoyDbContext context, IEnumerable<T> data)
        {
            var connection = context.GetConnection();
            connection.BulkUpdate(data);
        }

        public static void BulkUpdate<T>(this DbConnection connection, T[] data)
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

