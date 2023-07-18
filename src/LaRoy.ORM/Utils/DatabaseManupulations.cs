using MySql.Data.MySqlClient;
using MySql.Data.MySqlClient.X.XDevAPI.Common;
using Npgsql;
using Npgsql.Internal;
using NpgsqlTypes;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
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

        public static void UpdateDataFromTempTable<T>(this IDbConnection connection, string tableName, string tempTableName)
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
            command.ExecuteNonQuery();
        }

        public static IDataReader ExecuteDataReader(this IDbConnection connection, string sql, object param)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = sql;

            if (param != null)
                CommonHelper.AddParams(command, param);

            return command.ExecuteReader();
        }
    }
}
