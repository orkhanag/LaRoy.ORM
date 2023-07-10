using NpgsqlTypes;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net;
using System.Reflection;

namespace LaRoy.ORM.Utils
{
    public static class DataManupulations
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> data)
        {
            var tableName = typeof(T).Name;
            // Create a DataTable to hold the data
            DataTable dataTable = new DataTable(tableName);

            // Get the properties of the type T
            var properties = typeof(T).GetProperties();

            // Add columns to the DataTable based on the properties
            foreach (var property in properties)
                dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);

            // Add rows to the DataTable
            foreach (var item in data)
            {
                DataRow row = dataTable.NewRow();

                foreach (var property in properties)
                    row[property.Name] = property.GetValue(item) ?? DBNull.Value;

                dataTable.Rows.Add(row);
            }
            return dataTable;
        }

        public static string GetKeyFieldName<T>(this T type)
        {
            var properties = typeof(T).GetProperties();
            foreach (var prop in properties)
            {
                var attributes = prop.GetCustomAttributes(false);
                if (attributes.Where(x => x is KeyAttribute).Any())
                    return prop.Name;
            }
            throw new NotSupportedException($"Type {typeof(T).Name} doesn't have 'Key' field!");
        }
    }

}
