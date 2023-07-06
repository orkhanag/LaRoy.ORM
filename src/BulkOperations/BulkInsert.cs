using LaRoy.Mapper.BulkOperations.Utils;
using MySql.Data.MySqlClient;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;

namespace LaRoy.ORM.BulkOperations
{
    public static partial class BulkOperations
    {
        public static int BulkInsert<T>(this DbConnection connection, IEnumerable<T> data)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory(connection);
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                var tableName = typeof(T).Name;

                // Create a DataTable to hold the data
                DataTable dataTable = new DataTable(tableName);

                // Get the properties of the type T
                var properties = typeof(T).GetProperties();

                // Add columns to the DataTable based on the properties
                foreach (var property in properties)
                {
                    dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
                }

                // Add rows to the DataTable
                foreach (T item in data)
                {
                    DataRow row = dataTable.NewRow();
                    foreach (var property in properties)
                    {
                        row[property.Name] = property.GetValue(item) ?? DBNull.Value;
                    }
                    dataTable.Rows.Add(row);
                }

                // Create the data adapter and insert the data
                if (connection is SqlConnection sqlConnection)
                {
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnection))
                    {
                        bulkCopy.DestinationTableName = tableName;

                        // Map the columns in the DataTable to the destination table columns
                        for (int i = 0; i < dataTable.Columns.Count; i++)
                        {
                            bulkCopy.ColumnMappings.Add(i, i);
                        }

                        bulkCopy.WriteToServer(dataTable);
                    }
                }
                else if (connection is NpgsqlConnection npgConnection)
                {
                    using (var binaryImporter = npgConnection.BeginBinaryImport($"COPY {tableName} FROM STDIN (FORMAT BINARY)"))
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
                }
                else if (connection is MySqlConnection mySqlConnection)
                {
                    using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter())
                    {
                        MySqlCommandBuilder commandBuilder = new MySqlCommandBuilder(dataAdapter);

                        dataAdapter.SelectCommand = mySqlConnection.CreateCommand();
                        dataAdapter.SelectCommand.CommandText = $"SELECT * FROM {tableName}";
                        dataAdapter.InsertCommand = commandBuilder.GetInsertCommand();

                        // Perform the bulk insert
                        dataAdapter.Update(dataTable);
                    }
                }
                return dataTable.Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int BulkInsert<T>(this LaRoyDbContext context, IEnumerable<T> data)
        {
            var connection = context.GetConnection();
            return connection.BulkInsert(data);
        }

        public static int BulkInsert<T>(this DbConnection connection, T[] data)
        {
            var dataAsEnumerable = data.AsEnumerable();
            return BulkInsert<T>(connection, dataAsEnumerable);
        }

        public static int BulkInsert<T>(this LaRoyDbContext context, T[] data)
        {
            var connection = context.GetConnection();
            return connection.BulkInsert(data);
        }

        #region Helpers
        public static NpgsqlDbType GetNpgsqlDbType(this Type dataType)
        {
            if (dataType == typeof(string))
                return NpgsqlDbType.Text;
            if (dataType == typeof(int))
                return NpgsqlDbType.Integer;
            if (dataType == typeof(double))
                return NpgsqlDbType.Double;
            if (dataType == typeof(Single))
                return NpgsqlDbType.Numeric;
            if (dataType == typeof(DateTime))
                return NpgsqlDbType.Date;
            if (dataType == typeof(bool))
                return NpgsqlDbType.Boolean;
            if (dataType == typeof(float))
                return NpgsqlDbType.Integer;
            // Add additional type mappings as needed

            throw new NotSupportedException($"NpgsqlDbType mapping not found for type '{dataType.Name}'.");
        }

        #endregion
    }
}
