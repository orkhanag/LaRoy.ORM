using LaRoy.Mapper.BulkOperations.Utils;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace LaRoy.BulkOperations
{
    public static partial class BulkOperations
    {
        public static int BulkInsert<T>(this SqlConnection connection, IEnumerable<T> data)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            var sw = new Stopwatch();
            sw.Start();
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    var affRows = 0;
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                    {
                        bulkCopy.DestinationTableName = typeof(T).Name;
                        DataTable dataTable = new DataTable();
                        var properties = typeof(T).GetProperties();

                        foreach (var property in properties)
                        {
                            dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
                            bulkCopy.ColumnMappings.Add(property.Name, property.Name);
                        }

                        foreach (var item in data)
                        {
                            var values = new object[properties.Length];
                            for (int i = 0; i < properties.Length; i++)
                            {
                                values[i] = properties[i].GetValue(item);
                            }
                            dataTable.Rows.Add(values);
                        }

                        bulkCopy.WriteToServer(dataTable);
                        affRows = dataTable.Rows.Count;
                    }
                    transaction.Commit();
                    sw.Stop();
                    Console.WriteLine(sw.Elapsed);
                    return affRows;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public static int BulkInsert<T>(this LaRoyDbContext context, IEnumerable<T> data)
        {
            var connection = context.GetConnection();
            return BulkInsert(connection, data);
        }

        public static int BulkInsert<T>(this SqlConnection connection, T[] data)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            var sw = new Stopwatch();
            sw.Start();
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    var affRows = 0;
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                    {
                        bulkCopy.DestinationTableName = typeof(T).Name;
                        DataTable dataTable = new DataTable();
                        var properties = typeof(T).GetProperties();

                        foreach (var property in properties)
                        {
                            dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
                            bulkCopy.ColumnMappings.Add(property.Name, property.Name);
                        }

                        foreach (var item in data)
                        {
                            var values = new object[properties.Length];
                            for (int i = 0; i < properties.Length; i++)
                            {
                                values[i] = properties[i].GetValue(item);
                            }
                            dataTable.Rows.Add(values);
                        }

                        bulkCopy.WriteToServer(dataTable);
                        affRows = dataTable.Rows.Count;
                    }
                    transaction.Commit();
                    sw.Stop();
                    Console.WriteLine(sw.Elapsed);
                    return affRows;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public static int BulkInsert<T>(this LaRoyDbContext context, T[] data)
        {
            var connection = context.GetConnection();
            return BulkInsert(connection, data);
        }
    }
}
