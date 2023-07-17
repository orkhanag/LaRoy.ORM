using LaRoy.Mapper.BulkOperations.Utils;
using LaRoy.ORM.Utils;
using System.Data;

namespace LaRoy.ORM.Queries
{
    public static partial class Queries
    {
        public static IEnumerable<dynamic> Query(IDbConnection connection, string sql, object param = null)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                using var command = connection.CreateCommand();
                command.CommandText = sql;

                if (param != null)
                    CommonHelper.AddParams(command, param);

                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    yield return reader.ToExpandoObject();
                }
            }
            finally { connection.Close(); }
        }

        public static IEnumerable<dynamic> Query(LaRoyDbContext context, string sql, object param = null)
        {
            var connection = context.GetConnection();
            return Query(connection, sql, param);
        }

        public static dynamic QueryFirst(IDbConnection connection, string sql, object param = null)
        {
            var result = QueryFirstOrDefault(connection, sql, param);
            if (result != null) return result;
            else throw new InvalidOperationException("Query returned 0 element!");
        }

        public static dynamic QueryFirst(LaRoyDbContext context, string sql, object param = null)
        {
            var connection = context.GetConnection();
            return QueryFirst(connection, sql, param);
        }

        public static dynamic? QueryFirstOrDefault(IDbConnection connection, string sql, object param = null)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                using var command = connection.CreateCommand();
                command.CommandText = sql;

                if (param != null)
                    CommonHelper.AddParams(command, param);

                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    return reader.ToExpandoObject();
                }
                return null;
            }
            finally { connection.Close(); }
        }

        public static dynamic? QueryFirstOrDefault(LaRoyDbContext context, string sql, object param = null)
        {
            var connection = context.GetConnection();
            return QueryFirstOrDefault(connection, sql, param);
        }

        public static dynamic QuerySingle(IDbConnection connection, string sql, object param = null)
        {
            var result = QuerySingleOrDefault(connection, sql, param);
            if (result != null) return result;
            else throw new InvalidOperationException("Query returned 0 element!");
        }

        public static dynamic QuerySingle(LaRoyDbContext context, string sql, object param = null)
        {
            var connection = context.GetConnection();
            return QuerySingle(connection, sql, param);
        }

        public static dynamic? QuerySingleOrDefault(IDbConnection connection, string sql, object param = null)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                using var command = connection.CreateCommand();
                command.CommandText = sql;

                if (param != null)
                    CommonHelper.AddParams(command, param);

                List<dynamic> dataReaders = new();
                using IDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    dataReaders.Add(reader.ToExpandoObject());
                }

                if (dataReaders.Count == 1) return dataReaders.First();
                else if (dataReaders.Count == 0) return null;
                else throw new InvalidOperationException("Query returned more than 1 element!");
            }
            finally { connection.Close(); }
        }

        public static dynamic? QuerySingleOrDefault(LaRoyDbContext context, string sql, object param = null)
        {
            var connection = context.GetConnection();
            return QuerySingleOrDefault(connection, sql, param);
        }

        public static IEnumerable<T> Query<T>(IDbConnection connection, string sql, object param = null)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                using var command = connection.CreateCommand();
                command.CommandText = sql;

                if (param != null)
                    CommonHelper.AddParams(command, param);

                var properties = typeof(T).GetProperties();

                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    yield return reader.ToStrongType<T>();
                }
            }
            finally { connection.Close(); }
        }

        public static IEnumerable<T> Query<T>(LaRoyDbContext context, string sql, object param = null)
        {
            var connection = context.GetConnection();
            return Query<T>(connection, sql, param);
        }

        public static T QueryFirst<T>(IDbConnection connection, string sql, object param = null)
        {
            var result = QueryFirstOrDefault<T>(connection, sql, param);
            if (result != null) return result;
            else throw new InvalidOperationException("Query returned 0 element!");
        }

        public static T QueryFirst<T>(LaRoyDbContext context, string sql, object param = null)
        {
            var connection = context.GetConnection();
            return QueryFirst<T>(connection, sql, param);
        }

        public static T? QueryFirstOrDefault<T>(IDbConnection connection, string sql, object param = null)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                using var command = connection.CreateCommand();
                command.CommandText = sql;

                if (param != null)
                    CommonHelper.AddParams(command, param);

                var properties = typeof(T).GetProperties();

                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    return reader.ToStrongType<T>();
                }
                return default;
            }
            finally { connection.Close(); }
        }

        public static T? QueryFirstOrDefault<T>(LaRoyDbContext context, string sql, object param = null)
        {
            var connection = context.GetConnection();
            return QueryFirstOrDefault<T>(connection, sql, param);
        }

        public static T QuerySingle<T>(IDbConnection connection, string sql, object param = null)
        {
            var result = QuerySingleOrDefault<T>(connection, sql, param);
            if (result != null) return result;
            else throw new InvalidOperationException("Query returned 0 element!");
        }

        public static T QuerySingle<T>(LaRoyDbContext context, string sql, object param = null)
        {
            var connection = context.GetConnection();
            return QuerySingle<T>(connection, sql, param);
        }

        public static T? QuerySingleOrDefault<T>(IDbConnection connection, string sql, object param = null)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                using var command = connection.CreateCommand();
                command.CommandText = sql;

                if (param != null)
                    CommonHelper.AddParams(command, param);

                var properties = typeof(T).GetProperties();
                List<dynamic> dataReaders = new();
                using IDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    dataReaders.Add(reader.ToStrongType<T>());
                }

                if (dataReaders.Count == 1) return dataReaders.First();
                else if (dataReaders.Count == 0) return default;
                else throw new InvalidOperationException("Query returned more than 1 element!");
            }
            finally { connection.Close(); }
        }

        public static T? QuerySingleOrDefault<T>(LaRoyDbContext context, string sql, object param = null)
        {
            var connection = context.GetConnection();
            return QuerySingleOrDefault<T>(connection, sql, param);
        }
    }
}
