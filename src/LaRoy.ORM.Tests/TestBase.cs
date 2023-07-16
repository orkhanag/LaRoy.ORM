using Bogus;
using LaRoy.ORM.Tests.DTO;
using MySql.Data.MySqlClient;
using Npgsql;
using System.Data.SqlClient;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace LaRoy.ORM.Tests
{
    public class TestBase
    {
        public SqlConnection _sqlConnection = new("Server= localhost; Database= master; Integrated Security=True;");
        public NpgsqlConnection _npgSqlConnection = new("User ID=postgres;Password=admin;Host=localhost;Port=5432;Database=postgres;Pooling=true;Connection Lifetime=0;");
        public MySqlConnection _mySqlConnection = new("Server=localhost;Database=world;Uid=root;Pwd=admin;Pooling=False;");

        protected IEnumerable<DailyCustomerPayments>? GenerateTestData(int count)
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
    }
}
