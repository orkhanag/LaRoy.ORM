using Dapper;
using LaRoy.ORM.BulkOperations;
using Xunit;

namespace LaRoy.ORM.Tests.QueriesTests
{
    public class QueryTests : TestBase
    {
        [Fact]
        public void Query_ShouldReturnExpectedResultAccordingToSql_WhenSqlConnection_WithoutParameters()
        {
            //Arrange
            var data = GenerateTestData(10);
            var connection = _sqlConnection;
            var query = @"SELECT * FROM DailyCustomerPayments";
            connection.BulkInsert(data);
            //Act
            var result = Queries.Queries.Query(connection, query);
            var expected = data.Count();
            var actual = result.Count();
            connection.Execute("TRUNCATE TABLE DailyCustomerPayments");
            //Assert
            Assert.NotNull(result);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Query_ShouldReturnExpectedResultAccordingToSql_WhenSqlConnection_WithParameters()
        {
            //Arrange
            var data = GenerateTestData(10);
            var connection = _sqlConnection;
            var query = @"SELECT * FROM DailyCustomerPayments WHERE MobileNumber = @MobileNumber";
            connection.BulkInsert(data);
            //Act
            var result = Queries.Queries.Query(connection, query, new { MobileNumber = data.First().MobileNumber });
            var expected = data.First().MobileNumber;
            var actual = result.First().MobileNumber;
            connection.Execute("TRUNCATE TABLE DailyCustomerPayments");
            //Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(expected, actual);
        }
    }
}
