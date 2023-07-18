using LaRoy.Mapper.BulkOperations.Utils;
using LaRoy.ORM.Utils;
using System.Data;

namespace LaRoy.ORM.Queries
{
    public static partial class Queries
    {
        public static IEnumerable<dynamic> Query(this IDbConnection connection, string sql, object param = null)
        {
            try
            {
                using IDataReader reader = connection.ExecuteDataReader(sql, param);
                while (reader.Read())
                    yield return reader.ToExpandoObject();
            }
            finally { connection.Close(); }
        }

        public static IEnumerable<dynamic> Query(this LaRoyDbContext context, string sql, object param = null)
        {
            var connection = context.GetConnection();
            return Query(connection, sql, param);
        }

        public static dynamic QueryFirst(this IDbConnection connection, string sql, object param = null)
        {
            var result = QueryFirstOrDefault(connection, sql, param);
            if (result != null) return result;
            else throw new InvalidOperationException("Query returned 0 element!");
        }

        public static dynamic QueryFirst(this LaRoyDbContext context, string sql, object param = null)
        {
            var connection = context.GetConnection();
            return QueryFirst(connection, sql, param);
        }

        public static dynamic? QueryFirstOrDefault(this IDbConnection connection, string sql, object param = null)
        {
            try
            {
                using IDataReader reader = connection.ExecuteDataReader(sql, param);
                while (reader.Read())
                    return reader.ToExpandoObject();
                return null;
            }
            finally { connection.Close(); }
        }

        public static dynamic? QueryFirstOrDefault(this LaRoyDbContext context, string sql, object param = null)
        {
            var connection = context.GetConnection();
            return QueryFirstOrDefault(connection, sql, param);
        }

        public static dynamic QuerySingle(this IDbConnection connection, string sql, object param = null)
        {
            var result = QuerySingleOrDefault(connection, sql, param);
            if (result != null) return result;
            else throw new InvalidOperationException("Query returned 0 element!");
        }

        public static dynamic QuerySingle(this LaRoyDbContext context, string sql, object param = null)
        {
            var connection = context.GetConnection();
            return QuerySingle(connection, sql, param);
        }

        public static dynamic? QuerySingleOrDefault(this IDbConnection connection, string sql, object param = null)
        {
            try
            {
                using IDataReader reader = connection.ExecuteDataReader(sql, param);
                List<dynamic> data = new();
                while (reader.Read())
                    data.Add(reader.ToExpandoObject());
                if (data.Count == 1) return data.First();
                else if (data.Count == 0) return null;
                else throw new InvalidOperationException("Query returned more than 1 element!");
            }
            finally { connection.Close(); }
        }

        public static dynamic? QuerySingleOrDefault(this LaRoyDbContext context, string sql, object param = null)
        {
            var connection = context.GetConnection();
            return QuerySingleOrDefault(connection, sql, param);
        }

        public static IEnumerable<T> Query<T>(this IDbConnection connection, string sql, object param = null)
        {
            try
            {
                using IDataReader reader = connection.ExecuteDataReader(sql, param);
                while (reader.Read())
                    yield return reader.ToStrongType<T>();
            }
            finally { connection.Close(); }
        }

        public static IEnumerable<T> Query<T>(this LaRoyDbContext context, string sql, object param = null)
        {
            var connection = context.GetConnection();
            return Query<T>(connection, sql, param);
        }

        public static T QueryFirst<T>(this IDbConnection connection, string sql, object param = null)
        {
            var result = QueryFirstOrDefault<T>(connection, sql, param);
            if (result != null) return result;
            else throw new InvalidOperationException("Query returned 0 element!");
        }

        public static T QueryFirst<T>(this LaRoyDbContext context, string sql, object param = null)
        {
            var connection = context.GetConnection();
            return QueryFirst<T>(connection, sql, param);
        }

        public static T? QueryFirstOrDefault<T>(this IDbConnection connection, string sql, object param = null)
        {
            try
            {
                using IDataReader reader = connection.ExecuteDataReader(sql, param);
                while (reader.Read())
                    return reader.ToStrongType<T>();
                return default;
            }
            finally { connection.Close(); }
        }

        public static T? QueryFirstOrDefault<T>(this LaRoyDbContext context, string sql, object param = null)
        {
            var connection = context.GetConnection();
            return QueryFirstOrDefault<T>(connection, sql, param);
        }

        public static T QuerySingle<T>(this IDbConnection connection, string sql, object param = null)
        {
            var result = QuerySingleOrDefault<T>(connection, sql, param);
            if (result != null) return result;
            else throw new InvalidOperationException("Query returned 0 element!");
        }

        public static T QuerySingle<T>(this LaRoyDbContext context, string sql, object param = null)
        {
            var connection = context.GetConnection();
            return QuerySingle<T>(connection, sql, param);
        }

        public static T? QuerySingleOrDefault<T>(this IDbConnection connection, string sql, object param = null)
        {
            try
            {
                using IDataReader reader = connection.ExecuteDataReader(sql, param);
                List<dynamic> data = new();
                while (reader.Read())
                    data.Add(reader.ToStrongType<T>());
                if (data.Count == 1) return data.First();
                else if (data.Count == 0) return default;
                else throw new InvalidOperationException("Query returned more than 1 element!");
            }
            finally { connection.Close(); }
        }

        public static T? QuerySingleOrDefault<T>(this LaRoyDbContext context, string sql, object param = null)
        {
            var connection = context.GetConnection();
            return QuerySingleOrDefault<T>(connection, sql, param);
        }
    }
}
