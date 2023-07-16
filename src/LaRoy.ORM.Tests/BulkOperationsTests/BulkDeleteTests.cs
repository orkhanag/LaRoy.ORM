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
            var data = GenerateTestData(dataCount);
            var connection = _sqlConnection;
            connection.BulkInsert(data);
            var checkBeforeDelete = connection.ExecuteScalar("SELECT COUNT(*) FROM DailyCustomerPayments");
            //Act
            connection.BulkDelete(data);
            var checkAfterDelete = connection.ExecuteScalar("SELECT COUNT(*) FROM DailyCustomerPayments");
            //Assert
            Assert.Equal((int)checkBeforeDelete, data.Count());
            Assert.Equal((int)checkAfterDelete, 0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(69)]
        [InlineData(420)]
        public void BulkDelete_WithNpgSqlConnection_ShouldPerformCorrectOperation_AndReturnInsertedRowCount(int dataCount)
        {
            //Arrange
            var data = GenerateTestData(dataCount);
            var connection = _npgSqlConnection;
            connection.BulkInsert(data);
            var checkBeforeDelete = connection.ExecuteScalar("SELECT COUNT(*) FROM DailyCustomerPayments");
            //Act
            connection.BulkDelete(data);
            var checkAfterDelete = connection.ExecuteScalar("SELECT COUNT(*) FROM DailyCustomerPayments");
            //Assert
            Assert.Equal((Int64)checkBeforeDelete, data.Count());
            Assert.Equal((Int64)checkAfterDelete, 0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(69)]
        [InlineData(420)]
        public void BulkDelete_WithMySqlConnection_ShouldPerformCorrectOperation_AndReturnInsertedRowCount(int dataCount)
        {
            //Arrange
            var data = GenerateTestData(dataCount);
            var connection = _mySqlConnection;
            connection.BulkInsert(data);
            var checkBeforeDelete = connection.ExecuteScalar("SELECT COUNT(*) FROM DailyCustomerPayments");
            //Act
            connection.BulkDelete(data);
            var checkAfterDelete = connection.ExecuteScalar("SELECT COUNT(*) FROM DailyCustomerPayments");
            //Assert
            Assert.Equal((Int64)checkBeforeDelete, data.Count());
            Assert.Equal((Int64)checkAfterDelete, 0);
        }
    }
}
