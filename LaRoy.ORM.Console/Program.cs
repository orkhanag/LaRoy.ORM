using Bogus;
using Dapper;
using LaRoy.ORM.BulkOperations;
using LaRoy.ORM.Tests.DTO;
using MySql.Data.MySqlClient;
using Npgsql;
using System;
using System.Data;
using System.Data.SqlClient;

namespace LaRoy.ORM.Runner
{
    internal class Program
    {
        public static SqlConnection _sqlConnection = new("Server= localhost; Database= master; Integrated Security=True;");
        public static NpgsqlConnection _npgSqlConnection = new("User ID=postgres;Password=admin;Host=localhost;Port=5432;Database=postgres;Pooling=true;Connection Lifetime=0;");
        public static MySqlConnection _mySqlConnection = new("Server=localhost;Database=world;Uid=root;Pwd=admin;Pooling=False;");

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
            var data = GenerateTestData(10);
            var connection = _sqlConnection;
            var query = @"SELECT * FROM DailyCustomerPayments WHERE MobileNumber = @MobileNumber";
            connection.BulkInsert(data);
            var paramValue = data.First().MobileNumber;
            //Act
            var result = Query(connection, query, new { MobileNumber = paramValue });
            var expected = data.First().MobileNumber;
            var actual = result.First().MobileNumber;

            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }

        public static IEnumerable<dynamic> Query(IDbConnection connection, string sql, params object[] args)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = sql;

            var paramsCount = 0;
            if (args != null && args.Length > 0)
                foreach (var arg in args)
                {
                    var paramName = $"@{paramsCount++}";
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = paramName;
                    parameter.Value = arg;
                    command.Parameters.Add(parameter);
                }

            using var reader = command.ExecuteReader();

            var dataTable = new DataTable();
            dataTable.Load(reader);
            var list = dataTable.AsEnumerable();
            return list;


        }

        private class DataObject
        {
            public Dictionary<string, object> Properties { get; } = new Dictionary<string, object>();
        }
    }
}