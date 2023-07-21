using Dapper;
using LaRoy.ORM.BulkOperations;
using Xunit;


namespace LaRoy.ORM.Tests.BulkOperationsTests
{
    public class BulkInsertTests : TestBase
    {
        private readonly string _truncateQuery = "TRUNCATE TABLE DailyCustomerPayments";

        [Theory]
        [InlineData(420)]
        [InlineData(69)]
        [InlineData(1)]
        public void BulkInsert_WithSqlConnection_ShouldPerformCorrectOperation_AndReturnInsertedRowCount(int dataCount)
        {
            //Arrange
            var data = GenerateTestData(dataCount)!;

            //Act
            int result = _sqlConnection.BulkInsert(data);
            var checkData = (int)_sqlConnection.ExecuteScalar("SELECT COUNT(*) FROM DailyCustomerPayments");
            _sqlConnection.Execute(_truncateQuery);

            //Assert
            Assert.Equal(data.Count(), result);
            Assert.Equal(data.Count(), checkData);
        }

        [Theory]
        [InlineData(420)]
        [InlineData(69)]
        [InlineData(1)]
        public void BulkInsert_WithNpgSqlConnection_ShouldPerformCorrectOperation_AndReturnInsertedRowCount(int dataCount)
        {
            //Arrange
            var data = GenerateTestData(dataCount)!;

            //Act
            int result = _npgSqlConnection.BulkInsert(data);
            var checkData = (long)_npgSqlConnection.ExecuteScalar("SELECT COUNT(*) FROM DailyCustomerPayments");
            _npgSqlConnection.Execute(_truncateQuery);

            //Assert
            Assert.Equal(data.Count(), result);
            Assert.Equal(data.Count(), checkData);
        }

        [Theory]
        [InlineData(420)]
        [InlineData(69)]
        [InlineData(1)]
        public void BulkInsert_WithMySqlConnection_ShouldPerformCorrectOperation_AndReturnInsertedRowCount(int dataCount)
        {
            //Arrange
            var data = GenerateTestData(dataCount)!;

            //Act
            int result = _mySqlConnection.BulkInsert(data);
            var checkData = (long)_mySqlConnection.ExecuteScalar("SELECT COUNT(*) FROM DailyCustomerPayments");
            _mySqlConnection.Execute(_truncateQuery);

            //Assert
            Assert.Equal(data.Count(), result);
            Assert.Equal(data.Count(), checkData);
        }
    }
}
