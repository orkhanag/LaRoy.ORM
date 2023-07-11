using Bogus;
using LaRoy.ORM.BulkOperations;
using MySql.Data.MySqlClient;
using Npgsql;
using System.Data.SqlClient;
using Test.DTO_s;

namespace Test
{
    public class Program
    {
        static void Main(string[] args)
        {
            var _connection = new SqlConnection("server=10.10.10.144;database=KontaktHome;user id=kaya;password=18821882; Pooling = true; Max Pool Size = 2000;");
            var _npgConnection = new NpgsqlConnection("User ID=postgres;Password=admin;Host=localhost;Port=5432;Database=postgres;Pooling=true;Connection Lifetime=0;");
            var _mySqlConnection = new MySqlConnection("Server=localhost;Port=3306;Database=world;Uid=root;Pwd=admin;");
            var _faker = new Faker<DailyCustomerPayments>()
                    .RuleFor(x => x.PinCode, x => x.Person.Random.AlphaNumeric(7).ToUpper())
                    .RuleFor(x => x.InvoiceNumber, x => x.Random.Int(10_000_000, 99_999_999).ToString())
                    .RuleFor(x => x.MobileNumber, x => x.Phone.PhoneNumber("070#######"))
                    .RuleFor(x => x.RemainingDays, x => x.Random.ArrayElement<int>(new int[] { -10, -6, -3, -1 }))
                    .RuleFor(x => x.PayableAmount, x => x.Random.Int(1, 100))
                    .RuleFor(x => x.DelayStatus, x => x.Random.Bool())
                    .RuleFor(x => x.SendDate, x => DateTime.UtcNow);

            var data = _faker.GenerateLazy(10).ToList();

            var aa = _mySqlConnection.BulkInsert(data);

            var data2 = _faker.GenerateLazy(10).ToList();

            for (int i = 0; i < data2.Count(); i++)
            {
                data2[i].PinCode = data[i].PinCode;
            }

            _mySqlConnection.BulkUpdate(data2);
        }
    }
}

