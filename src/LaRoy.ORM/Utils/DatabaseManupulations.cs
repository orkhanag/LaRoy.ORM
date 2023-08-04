using MySql.Data.MySqlClient;
using MySql.Data.MySqlClient.X.XDevAPI.Common;
using Npgsql;
using Npgsql.Internal;
using NpgsqlTypes;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Dynamic;
using System.Net;

namespace LaRoy.ORM.Utils
{
    public static class DatabaseManupulations
    {
        public static void CreateTemporaryTable<T>(this IDbConnection connection, string tableName, bool onlyKeyField = false)
        {
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = CommonHelper.GenerateCreateTableQuery<T>(tableName, connection, onlyKeyField);
                command.ExecuteNonQuery();
            }
        }

        public static void SqlBulkInsert(this SqlConnection sqlConnection, string tableName, DataTable dataTable)
        {
            using SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnection);
            bulkCopy.DestinationTableName = tableName;
            for (int i = 0; i < dataTable.Columns.Count; i++)
                bulkCopy.ColumnMappings.Add(i, i);
            bulkCopy.WriteToServer(dataTable);
        }

        public static void NpgSqlBulkInsert(this NpgsqlConnection npgsqlConnection, string tableName, DataTable dataTable)
        {
            using var binaryImporter = npgsqlConnection.BeginBinaryImport($"COPY {tableName} FROM STDIN (FORMAT BINARY)");
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

        public static void MySqlBulkInsert(this MySqlConnection mySqlConnection, string tableName, DataTable dataTable)
        {
            using MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
            MySqlCommandBuilder commandBuilder = new MySqlCommandBuilder(dataAdapter);
            dataAdapter.SelectCommand = mySqlConnection.CreateCommand();
            dataAdapter.SelectCommand.CommandText = $"SELECT * FROM {tableName}";
            dataAdapter.InsertCommand = commandBuilder.GetInsertCommand();
            dataAdapter.Update(dataTable);
        }

        public static int UpdateDataFromTempTable<T>(this IDbConnection connection, string tableName, string tempTableName)
        {
            using IDbCommand command = connection.CreateCommand();
            var properties = typeof(T).GetProperties();
            var columnValues = string.Empty;
            if (command is SqlCommand)
                foreach (var prop in properties)
                    columnValues += $"{prop.Name} = tmp.{prop.Name},";
            else if (command is NpgsqlCommand)
                foreach (var prop in properties)
                    columnValues += $"{prop.Name} = tmp.{prop.Name},";
            else if (command is MySqlCommand)
                foreach (var prop in properties)
                    columnValues += $"dest.{prop.Name} = src.{prop.Name},";
            var keyFieldName = CommonHelper.GetKeyField<T>().Name;
            command.CommandTimeout = 300;
            command.CommandText = command.GetSpecificUpdateCommandText(tableName, columnValues.Trim(','), tempTableName, keyFieldName);
            return command.ExecuteNonQuery();
        }

        public static DbDataReader ExecuteDataReader(this IDbConnection connection, string sql, object? param = null)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = sql;

            if (param != null)
                CommonHelper.AddParams(command, param);

            return (DbDataReader)command.ExecuteReader();
        }

        public async static Task<DbDataReader> ExecuteDataReaderAsync(this IDbConnection connection, string sql, object? param = null)
        {

            if (connection.State != ConnectionState.Open)
                await connection.TryOpenAsync().ConfigureAwait(false);

            using var command = connection.TryCreateCommandAsync();
            command.CommandText = sql;

            if (param != null)
                CommonHelper.AddParams(command, param);

            return await command.ExecuteReaderAsync();
        }

        public static Task TryOpenAsync(this IDbConnection cnn)
        {
            if (cnn is DbConnection dbCnn)
                return dbCnn.OpenAsync();
            else
                throw new InvalidOperationException("Async operations require use of a DbConnection or an already open IDbConnection");
        }

        public static DbCommand TryCreateCommandAsync(this IDbConnection cnn)
        {
            if (cnn.CreateCommand() is DbCommand dbCommand)
                return dbCommand;
            else
                throw new InvalidOperationException("Async operations require use of a DbConnection");
        }

        public static IEnumerable<T> QueryImpl<T>(IDbConnection cnn, string query, object? param = null, bool isStrictType = false)
        {
            DbDataReader? reader = null;
            try
            {
                if (cnn.State != ConnectionState.Open) cnn.Open();
                reader = cnn.ExecuteDataReader(query, param);
                if (!isStrictType)
                    while (reader.Read())
                        yield return reader.ToExpandoObject();
                else
                    while (reader.Read())
                        yield return reader.ToStrongType<T>();
            }
            finally
            {
                if (reader is not null)
                    reader.Dispose();
            }
        }

        public static async IAsyncEnumerable<T> QueryImplAsync<T>(IDbConnection cnn, string query, object? param = null, bool isStrictType = false)
        {
            DbDataReader? reader = null;
            try
            {
                if (cnn.State != ConnectionState.Open) cnn.Open();
                reader = await cnn.ExecuteDataReaderAsync(query, param);
                if (!isStrictType)
                    while (reader.Read())

                        yield return reader.ToExpandoObject();
                else
                    while (reader.Read())
                        yield return reader.ToStrongType<T>();
            }
            finally
            {
                if (reader is not null)
                    reader.Dispose();
            }
        }
    }
}
