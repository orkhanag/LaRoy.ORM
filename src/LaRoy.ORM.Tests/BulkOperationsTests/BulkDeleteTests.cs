using Dapper;
using LaRoy.ORM.BulkOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LaRoy.ORM.Tests.BulkOperationsTests
{
    public class BulkDeleteTests : TestBase
    {
        [Theory]
        [InlineData(1)]
        [InlineData(69)]
        [InlineData(420)]
        public void BulkDelete_WithSqlConnection_ShouldPerformCorrectOperation_AndReturnInsertedRowCount(int dataCount)
        {
            //Arrange
            var data = GenerateTestData(dataCount)!;
            _sqlConnection.BulkInsert(data);

            var query = "SELECT COUNT(*) FROM DailyCustomerPayments";
            var checkBeforeDelete = (int)_sqlConnection.ExecuteScalar(query);

            //Act
            _sqlConnection.BulkDelete(data);
            var checkAfterDelete = (int)_sqlConnection.ExecuteScalar(query);

            //Assert
            Assert.Equal(data.Count(), checkBeforeDelete);
            Assert.Equal(0, checkAfterDelete);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(69)]
        [InlineData(420)]
        public void BulkDelete_WithNpgSqlConnection_ShouldPerformCorrectOperation_AndReturnInsertedRowCount(int dataCount)
        {
            //Arrange
            var data = GenerateTestData(dataCount)!;
            _npgSqlConnection.BulkInsert(data);

            var query = "SELECT COUNT(*) FROM DailyCustomerPayments";
            var checkBeforeDelete = (long)_npgSqlConnection.ExecuteScalar(query);

            //Act
            _npgSqlConnection.BulkDelete(data);
            var checkAfterDelete = (long)_npgSqlConnection.ExecuteScalar(query);

            //Assert
            Assert.Equal(data.Count(), checkBeforeDelete);
            Assert.Equal(0, checkAfterDelete);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(69)]
        [InlineData(420)]
        public void BulkDelete_WithMySqlConnection_ShouldPerformCorrectOperation_AndReturnInsertedRowCount(int dataCount)
        {
            //Arrange
            var data = GenerateTestData(dataCount)!;
            _mySqlConnection.BulkInsert(data);

            var query = "SELECT COUNT(*) FROM DailyCustomerPayments";
            var checkBeforeDelete = (long)_mySqlConnection.ExecuteScalar(query);

            //Act
            _mySqlConnection.BulkDelete(data);
            var checkAfterDelete = (long)_mySqlConnection.ExecuteScalar(query);

            //Assert
            Assert.Equal(data.Count(), checkBeforeDelete);
            Assert.Equal(0, checkAfterDelete);
        }
    }
}
