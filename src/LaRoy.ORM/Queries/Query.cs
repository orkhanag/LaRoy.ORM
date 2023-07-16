using System.Data;

namespace LaRoy.ORM.Queries
{
    public static partial class Queries
    {
        public static IEnumerable<dynamic> Query(this IDbConnection connection, string sql, params object[] args)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = sql;

            if (args != null && args.Length > 0)
                foreach (var arg in args)
                {
                    var paramName = $"@{command.Parameters.Count}";
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = paramName;
                    parameter.Value = arg;
                    command.Parameters.Add(parameter);
                }

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var dataObject = new DataObject();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var propertyName = reader.GetName(i);
                    var propertyValue = reader.GetValue(i);
                    dataObject.Properties.Add(propertyName, propertyValue);
                }

                yield return dataObject;
            }
        }

        private class DataObject
        {
            public Dictionary<string, object> Properties { get; } = new Dictionary<string, object>();
        }
    }
}
