using LaRoy.ORM.Utils;
using System.Data;

namespace LaRoy.ORM.Queries
{
    public static partial class Queries
    {
        public static async Task<IEnumerable<dynamic>?> QueryAsync(this IDbConnection connection, string query, object? param = null, bool buffered = false)
        {
            var data = DatabaseManupulations.QueryImplAsync<dynamic>(connection, query, param, false);
            return buffered ? data.ToListAsync().Result : data.ToEnumerable();
        }

        public static Task<IEnumerable<dynamic>?> QueryAsync(this LaRoyDbContext context, string query, object? param = null, bool buffered = false)
        {
            var connection = context.GetConnection();
            return QueryAsync(connection, query, param, buffered);
        }

        public static async Task<dynamic> QueryFirstAsync(this IDbConnection connection, string query, object? param = null)
        {
            var result = await QueryFirstOrDefaultAsync(connection, query, param);
            if (result != null) return result;
            else throw new InvalidOperationException("Query returned 0 element!");
        }

        public static Task<dynamic> QueryFirstAsync(this LaRoyDbContext context, string query, object? param = null)
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

        public static Task<dynamic?> QueryFirstOrDefaultAsync(this LaRoyDbContext context, string query, object? param = null)
        {
            var connection = context.GetConnection();
            return QueryFirstOrDefaultAsync(connection, query, param);
        }

        public static async Task<dynamic> QuerySingleAsync(this IDbConnection connection, string query, object? param = null)
        {
            var result = await QuerySingleOrDefaultAsync(connection, query, param);
            if (result != null) return result;
            else throw new InvalidOperationException("Query returned 0 element!");
        }

        public static Task<dynamic> QuerySingleAsync(this LaRoyDbContext context, string query, object? param = null)
        {
            var connection = context.GetConnection();
            return QuerySingleAsync(connection, query, param);
        }

        public static async Task<dynamic?> QuerySingleOrDefaultAsync(this IDbConnection connection, string query, object? param = null)
        {
            try
            {
                using var reader = await connection.ExecuteDataReaderAsync(query, param);
                List<dynamic> data = new();
                while (reader.Read())
                    data.Add(reader.ToExpandoObject());
                if (data.Count == 1) return data.First();
                else if (data.Count == 0) return null;
                else throw new InvalidOperationException("Query returned more than 1 element!");
            }
            finally { connection.Close(); }
        }
        public static Task<dynamic?> QuerySingleOrDefaultAsync(this LaRoyDbContext context, string query, object? param = null)
        {
            var connection = context.GetConnection();
            return QuerySingleOrDefaultAsync(connection, query, param);
        }

        public static async Task<IEnumerable<T>?> QueryAsync<T>(this IDbConnection connection, string query, object? param = null, bool buffered = false)
        {
            var data = DatabaseManupulations.QueryImplAsync<T>(connection, query, param, isStrictType: true);
            return buffered ? await data.ToListAsync() : data.ToEnumerable();
        }

        public static Task<IEnumerable<T>?> QueryAsync<T>(this LaRoyDbContext context, string query, object? param = null, bool buffered = false)
        {
            var connection = context.GetConnection();
            return QueryAsync<T>(connection, query, param, buffered);
        }

        public static Task<T> QueryFirstAsync<T>(this IDbConnection connection, string query, object? param = null)
        {
            var result = QueryFirstOrDefaultAsync<T>(connection, query, param);
            if (result != null) return result;
            else throw new InvalidOperationException("Query returned 0 element!");
        }

        public static Task<T> QueryFirstAsync<T>(this LaRoyDbContext context, string query, object? param = null)
        {
            var connection = context.GetConnection();
            return QueryFirstAsync<T>(connection, query, param);
        }

        public static async Task<T?> QueryFirstOrDefaultAsync<T>(this IDbConnection connection, string query, object? param = null)
        {
            try
            {
                using IDataReader reader = await connection.ExecuteDataReaderAsync(query, param);
                while (reader.Read())
                    return reader.ToStrongType<T>();
                return default;
            }
            finally { connection.Close(); }
        }

        public static Task<T?> QueryFirstOrDefaultAsync<T>(this LaRoyDbContext context, string query, object? param = null)
        {
            var connection = context.GetConnection();
            return QueryFirstOrDefaultAsync<T>(connection, query, param);
        }

        public static async Task<T> QuerySingleAsync<T>(this IDbConnection connection, string query, object? param = null)
        {
            var result = await QuerySingleOrDefaultAsync<T>(connection, query, param);
            if (result != null) return result;
            else throw new InvalidOperationException("Query returned 0 element!");
        }

        public static Task<T> QuerySingleAsync<T>(this LaRoyDbContext context, string query, object? param = null)
        {
            var connection = context.GetConnection();
            return QuerySingleAsync<T>(connection, query, param);
        }

        public static async Task<T?> QuerySingleOrDefaultAsync<T>(this IDbConnection connection, string query, object? param = null)
        {
            try
            {
                using IDataReader reader = await connection.ExecuteDataReaderAsync(query, param);
                List<dynamic> data = new();
                while (reader.Read())
                    data.Add(reader.ToStrongType<T>());
                if (data.Count == 1) return data.First();
                else if (data.Count == 0) return default;
                else throw new InvalidOperationException("Query returned more than 1 element!");
            }
            finally { connection.Close(); }
        }

        public static Task<T?> QuerySingleOrDefaultAsync<T>(this LaRoyDbContext context, string query, object? param = null)
        {
            var connection = context.GetConnection();
            return QuerySingleOrDefaultAsync<T>(connection, query, param);
        }
    }
}
