using LaRoy.ORM.Utils;
using System.Data;
using System.Data.Common;

namespace LaRoy.ORM.Queries
{
    public static partial class Query
    {
        public static async Task<IEnumerable<dynamic>> QueryAsync(this IDbConnection connection, string query, object? param = null, bool buffered = false)
        {
            DbDataReader reader = null;
            try
            {
                reader = await connection.ExecuteDataReaderAsync(query, param);
                if (buffered)
                {
                    var buffer = new List<dynamic>();

                    while (await reader.ReadAsync())
                        buffer.Add(reader.ToExpandoObject());

                    return buffer;
                }
                else
                {
                    var deffered = reader.GetDefferedResult<dynamic>();
                    reader = null;
                    return deffered;
                }
            }
            finally
            {
                if(reader is not null)
                    reader.Dispose();
                connection.Close();
            }
        }

        public static Task<IEnumerable<dynamic>> QueryAsync(this LaRoyDbContext context, string query, object? param = null, bool buffered = false)
        {
            var connection = context.GetConnection();
            return QueryAsync(connection, query, param, buffered);
        }

        public static dynamic QueryFirstAsync(this IDbConnection connection, string query, object? param = null)
        {
            var result = QueryFirstOrDefaultAsync(connection, query, param);
            if (result != null) return result;
            else throw new InvalidOperationException("Query returned 0 element!");
        }

        public static dynamic QueryFirstAsync(this LaRoyDbContext context, string query, object? param = null)
        {
            var connection = context.GetConnection();
            return QueryFirstAsync(connection, query, param);
        }

        public static async Task<dynamic?> QueryFirstOrDefaultAsync(this IDbConnection connection, string query, object? param = null)
        {
            try
            {
                using var reader = await connection.ExecuteDataReaderAsync(query, param);
                while (await reader.ReadAsync())
                    return reader.ToExpandoObject();
                return null;
            }
            finally { connection.Close(); }
        }

        public static dynamic? QueryFirstOrDefaultAsync(this LaRoyDbContext context, string query, object? param = null)
        {
            var connection = context.GetConnection();
            return QueryFirstOrDefaultAsync(connection, query, param);
        }
    }
}
