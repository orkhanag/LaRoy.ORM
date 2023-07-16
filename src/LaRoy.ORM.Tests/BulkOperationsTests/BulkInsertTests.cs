using Dapper;
using LaRoy.ORM.BulkOperations;
using Xunit;


namespace LaRoy.ORM.Tests.BulkOperationsTests
{
    public class BulkInsertTests : TestBase
    {
        [Theory]
        [InlineData(4269)]
        [InlineData(420)]
        [InlineData(69)]
        [InlineData(1)]
        public void BulkInsert_WithSqlConnection_ShouldPerformCorrectOperation_AndReturnInsertedRowCount(int dataCount)
        {
            //Arrange
            var data = GenerateTestData(dataCount);
            var connection = _sqlConnection;
            //Act
            int result = connection.BulkInsert(data);
            var checkData = connection.ExecuteScalar("SELECT COUNT(*) FROM DailyCustomerPayments");
            connection.Execute("TRUNCATE TABLE DailyCustomerPayments");
            //Assert
            Assert.Equal(data.Count(), result);
            Assert.Equal(data.Count(), (int)checkData);
        }

        [Theory]
        [InlineData(4269)]
        [InlineData(420)]
        [InlineData(69)]
        [InlineData(1)]
        public void BulkInsert_WithNpgSqlConnection_ShouldPerformCorrectOperation_AndReturnInsertedRowCount(int dataCount)
        {
            //Arrange
            var data = GenerateTestData(dataCount);
            var connection = _npgSqlConnection;
            //Act
            int result = connection.BulkInsert(data);
            var checkData = connection.ExecuteScalar("SELECT COUNT(*) FROM DailyCustomerPayments");
            connection.Execute("TRUNCATE TABLE DailyCustomerPayments");
            //Assert
            Assert.Equal(data.Count(), result);
            Assert.Equal(data.Count(), (Int64)checkData);
        }

        [Theory]
        [InlineData(4269)]
        [InlineData(420)]
        [InlineData(69)]
        [InlineData(1)]
        public void BulkInsert_WithMySqlConnection_ShouldPerformCorrectOperation_AndReturnInsertedRowCount(int dataCount)
        {
            //Arrange
            var data = GenerateTestData(dataCount);
            var connection = _mySqlConnection;
            //Act
            int result = connection.BulkInsert(data);
            var checkData = connection.ExecuteScalar("SELECT COUNT(*) FROM DailyCustomerPayments");
            connection.Execute("TRUNCATE TABLE DailyCustomerPayments");
            //Assert
            Assert.Equal(data.Count(), result);
            Assert.Equal(data.Count(), (Int64)checkData);
        }
    }
}
