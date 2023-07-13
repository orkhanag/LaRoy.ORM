using System.ComponentModel.DataAnnotations;
using System.Data;

namespace LaRoy.ORM.Utils
{
    public static class DataManupulations
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> data)
        {
            var tableName = typeof(T).Name;
            DataTable dataTable = new DataTable(tableName);

            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
                dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);

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
