using Bogus;
using Dapper;
using LaRoy.ORM.BulkOperations;
using LaRoy.ORM.Queries;
using LaRoy.ORM.Tests.DTO;
using LaRoy.ORM.Utils;
using MySql.Data.MySqlClient;
using Npgsql;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Reflection;
using Xunit;

namespace LaRoy.ORM.Runner
{
    internal class Program
    {
        public static SqlConnection _sqlConnection = new("Server= localhost; Database= master; Integrated Security=True;");
        public static NpgsqlConnection _npgSqlConnection = new("User ID=postgres;Password=admin;Host=localhost;Port=5432;Database=postgres;Pooling=true;Connection Lifetime=0;");
        public static MySqlConnection _mySqlConnection = new("Server=localhost;Database=world;Uid=root;Pwd=admin;Pooling=False;");
        private static readonly string _truncateQuery = "TRUNCATE TABLE DailyCustomerPayments";

        protected static IEnumerable<DailyCustomerPayments>? GenerateTestData(int count)
        {
            var faker = new Faker<DailyCustomerPayments>()
                    .RuleFor(x => x.PinCode, x => x.Person.Random.AlphaNumeric(7).ToUpper())
                    .RuleFor(x => x.InvoiceNumber, x => x.Random.Int(10_000_000, 99_999_999).ToString())
                    .RuleFor(x => x.MobileNumber, x => x.Phone.PhoneNumber("070#######"))
                    .RuleFor(x => x.RemainingDays, x => x.Random.ArrayElement<int>(new int[] { -10, -6, -3, -1 }))
                    .RuleFor(x => x.PayableAmount, x => x.Random.Int(1, 100))
                    .RuleFor(x => x.DelayStatus, x => x.Random.Bool())
                    .RuleFor(x => x.SendDate, x => DateTime.UtcNow);

            return faker.Generate(count);
        }
        static void Main(string[] args)
        {
            //Arrange
            var data = GenerateTestData(10);
            var connection = _sqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            connection.BulkInsert(data);
            var paramValue = data.First().PinCode;
            //Act
            var result = QuerySingle(connection, query, new { PinCode = paramValue });
            //Assert
            Assert.Equal(result.MobileNumber, data.Single(x => x.PinCode == paramValue).MobileNumber);
            //Clean
            connection.Execute(_truncateQuery);
        }

        public static dynamic QuerySingle(IDbConnection connection, string sql, object param = null)
        {
            var result = QuerySingleOrDefault(connection, sql, param);
            if (result != null) return result;
            else throw new InvalidOperationException("Query returned 0 element!");
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
    }
}