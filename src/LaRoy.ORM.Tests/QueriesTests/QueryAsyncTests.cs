using Dapper;
using LaRoy.ORM.BulkOperations;
using LaRoy.ORM.Tests.DTO;
using Xunit;

namespace LaRoy.ORM.Tests.QueriesTests
{
    public class QueryAsyncTests : TestBase
    {
        #region Dynamic QueryAsync
        [Fact]
        public async void QueryAsync_ShouldReturnExpectedResultAccordingToSql_WhenSqlConnection_WithoutParameters()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _sqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments";
            connection.BulkInsert(data);

            //Act
            var result = await Queries.Queries.QueryAsync(connection, query);
            var expected = data.Count();
            var actual = result.Count();

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(expected, actual);

            //Clean
            connection.Execute(_truncateQuery);
        }
        [Fact]
        public async void QueryAsync_ShouldReturnExpectedResultAccordingToSql_WhenSqlConnection_WithParameters()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _sqlConnection;
            var query = @"SELECT * FROM DailyCustomerPayments WHERE MobileNumber = @MobileNumber";
            connection.BulkInsert(data);

            //Act
            var result = await Queries.Queries.QueryAsync(connection, query, new { MobileNumber = data.First().MobileNumber });
            var expected = data.First().MobileNumber;
            var actual = result.First().MobileNumber;

            //Assert
            Assert.Single(result);
            Assert.Equal(expected, actual);

            //Clean
            connection.Execute(_truncateQuery);
        }
        [Fact]
        public async void QueryAsync_ShouldReturnExpectedResultAccordingToSql_WhenNpgSqlConnection_WithoutParameters()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _npgSqlConnection;
            var query = @"SELECT * FROM DailyCustomerPayments";
            connection.BulkInsert(data);

            //Act
            var result = await Queries.Queries.QueryAsync(connection, query);
            var expected = data.Count();
            var actual = result.Count();

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(expected, actual);

            //Clean
            connection.Execute(_truncateQuery);
        }
        [Fact]
        public async void QueryAsync_ShouldReturnExpectedResultAccordingToSql_WhenNpgSqlConnection_WithParameters()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _npgSqlConnection;
            var query = @"SELECT * FROM DailyCustomerPayments WHERE MobileNumber = @MobileNumber";
            connection.BulkInsert(data);

            //Act
            var result = await Queries.Queries.QueryAsync(connection, query, new { MobileNumber = data.First().MobileNumber });
            var expected = data.First().MobileNumber;
            var actual = result.First().mobilenumber;

            //Assert
            Assert.Single(result);
            Assert.Equal(expected, actual);

            //Clean
            connection.Execute(_truncateQuery);
        }
        [Fact]
        public async void QueryAsync_ShouldReturnExpectedResultAccordingToSql_WhenMySqlConnection_WithoutParameters()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _mySqlConnection;
            var query = @"SELECT * FROM DailyCustomerPayments";
            connection.BulkInsert(data);

            //Act
            var result = await Queries.Queries.QueryAsync(connection, query);
            var expected = data.Count();
            var actual = result.Count();

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(expected, actual);

            //Clean
            connection.Execute(_truncateQuery);
        }
        [Fact]
        public async void QueryAsync_ShouldReturnExpectedResultAccordingToSql_WhenMySqlConnection_WithParameters()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _mySqlConnection;
            var query = @"SELECT * FROM DailyCustomerPayments WHERE MobileNumber = @MobileNumber";
            connection.BulkInsert(data);

            //Act
            var result = await Queries.Queries.QueryAsync(connection, query, new { MobileNumber = data.First().MobileNumber });
            var expected = data.First().MobileNumber;
            var actual = result.First().MobileNumber;

            //Assert
            Assert.Single(result);
            Assert.Equal(expected, actual);

            //Clean
            connection.Execute(_truncateQuery);
        }
        #endregion

        #region StrongType QueryAsync
        [Fact]
        public async void StrongTypeQueryAsync_ShouldReturnExpectedResultAccordingToSql_WhenSqlConnection_WithoutParameters()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _sqlConnection;
            var query = @"SELECT * FROM DailyCustomerPayments";
            connection.BulkInsert(data);

            //Act
            var result = await Queries.Queries.QueryAsync<DailyCustomerPayments>(connection, query);
            var expected = data.Count();
            var actual = result.Count();
            var type = result.First();

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(expected, actual);
            Assert.IsType<DailyCustomerPayments>(type);

            //Clean
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public async void StrongTypeQueryAsync_ShouldReturnExpectedResultAccordingToSql_WhenSqlConnection_WithParameters()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _sqlConnection;
            var query = @"SELECT * FROM DailyCustomerPayments WHERE MobileNumber = @MobileNumber";
            connection.BulkInsert(data);

            //Act
            var result = await Queries.Queries.QueryAsync<DailyCustomerPayments>(connection, query, new { MobileNumber = data.First().MobileNumber });
            var expected = data.First().MobileNumber;
            var actual = result.First().MobileNumber;
            var type = result.First();

            //Assert
            Assert.Single(result);
            Assert.Equal(expected, actual);
            Assert.IsType<DailyCustomerPayments>(type);

            //Clean
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public async void StrongTypeQueryAsync_ShouldReturnExpectedResultAccordingToSql_WhenNpgSqlConnection_WithoutParameters()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _npgSqlConnection;
            var query = @"SELECT * FROM DailyCustomerPayments";
            connection.BulkInsert(data);

            //Act
            var result = await Queries.Queries.QueryAsync<DailyCustomerPayments>(connection, query);
            var expected = data.Count();
            var actual = result.Count();
            var type = result.First();

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(expected, actual);
            Assert.IsType<DailyCustomerPayments>(type);

            //Clean
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public async void StrongTypeQueryAsync_ShouldReturnExpectedResultAccordingToSql_WhenNpgSqlConnection_WithParameters()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _npgSqlConnection;
            var query = @"SELECT * FROM DailyCustomerPayments WHERE MobileNumber = @MobileNumber";
            connection.BulkInsert(data);

            //Act
            var result = await Queries.Queries.QueryAsync<DailyCustomerPayments>(connection, query, new { MobileNumber = data.First().MobileNumber });
            var expected = data.First().MobileNumber;
            var actual = result.First().MobileNumber;
            var type = result.First();

            //Assert
            Assert.Single(result);
            Assert.Equal(expected, actual);
            Assert.IsType<DailyCustomerPayments>(type);
            //Clean
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public async void StrongTypeQueryAsync_ShouldReturnExpectedResultAccordingToSql_WhenMySqlConnection_WithoutParameters()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _mySqlConnection;
            var query = @"SELECT * FROM DailyCustomerPayments";
            connection.BulkInsert(data);

            //Act
            var result = await Queries.Queries.QueryAsync<DailyCustomerPayments>(connection, query);
            var expected = data.Count();
            var actual = result.Count();
            var type = result.FirstOrDefault();

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(expected, actual);
            Assert.IsType<DailyCustomerPayments>(type);

            //Clean
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public async void StrongTypeQueryAsync_ShouldReturnExpectedResultAccordingToSql_WhenMySqlConnection_WithParameters()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _mySqlConnection;
            var query = @"SELECT * FROM DailyCustomerPayments WHERE MobileNumber = @MobileNumber";
            connection.BulkInsert(data);

            //Act
            var result = await Queries.Queries.QueryAsync<DailyCustomerPayments>(connection, query, new { MobileNumber = data.First().MobileNumber });
            var expected = data.First().MobileNumber;
            var actual = result.First().MobileNumber;
            var type = result.FirstOrDefault();

            //Assert
            Assert.Single(result);
            Assert.Equal(expected, actual);
            Assert.IsType<DailyCustomerPayments>(type);

            //Clean
            connection.Execute(_truncateQuery);
        }
        #endregion

        #region Dynamic QerySingleAsync
        [Fact]
        public async void QuerySingleAsync_ShouldReturnSingleResultAccordingToSql_WithSqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _sqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            connection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = await Queries.Queries.QuerySingleAsync(connection, query, new { PinCode = paramValue });
            var actual = data.First(x => x.PinCode == paramValue).MobileNumber;
            var expected = result.MobileNumber;

            //Assert
            Assert.Equal(expected, actual);

            //Clean
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public async void QuerySingleAsync_ShouldThrowException_WhenZeroElementReturnedFromQery_WithSqlConnection()
        {
            //Arrange
            var connection = _sqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            var paramValue = "1Q2W3E4";

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => Queries.Queries.QuerySingleAsync(connection, query, new { PinCode = paramValue }));
        }

        [Fact]
        public async void QuerySingleAsync_ShouldThrowException_WhenMoreThanOneElementsReturnedFromQery_WithSqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _sqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments";
            connection.BulkInsert(data);

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => Queries.Queries.QuerySingleAsync(connection, query));
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public async void QuerySingleAsync_ShouldReturnSingleResultAccordingToSql_WithNpgSqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _npgSqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            connection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = await Queries.Queries.QuerySingleAsync(connection, query, new { PinCode = paramValue });

            //Assert
            Assert.Equal(data.First(x => x.PinCode == paramValue).MobileNumber, result.mobilenumber);

            //Clean
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public async void QuerySingleAsync_ShouldThrowException_WhenZeroElementReturnedFromQery_WithNpgSqlConnection()
        {
            //Arrange
            var connection = _npgSqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            var paramValue = "1Q2W3E4";

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => Queries.Queries.QuerySingleAsync(connection, query, new { PinCode = paramValue }));
        }

        [Fact]
        public async void QuerySingleAsync_ShouldThrowException_WhenMoreThanOneElementsReturnedFromQery_WithNpgSqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _npgSqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments";
            connection.BulkInsert(data);

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => Queries.Queries.QuerySingleAsync(connection, query));
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public async void QuerySingleAsync_ShouldReturnSingleResultAccordingToSql_WithMySqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _mySqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            connection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = await Queries.Queries.QuerySingleAsync(connection, query, new { PinCode = paramValue });

            //Assert
            Assert.Equal(data.First(x => x.PinCode == paramValue).MobileNumber, result.MobileNumber);

            //Clean
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public async void QuerySingleAsync_ShouldThrowException_WhenZeroElementReturnedFromQery_WithMySqlConnection()
        {
            //Arrange
            var connection = _mySqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            var paramValue = "1Q2W3E4";

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => Queries.Queries.QuerySingleAsync(connection, query, new { PinCode = paramValue }));
        }

        [Fact]
        public async void QuerySingleAsync_ShouldThrowException_WhenMoreThanOneElementsReturnedFromQery_WithMySqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _mySqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments";
            connection.BulkInsert(data);

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => Queries.Queries.QuerySingleAsync(connection, query));
            connection.Execute(_truncateQuery);
        }
        #endregion

        #region StrongType QuerySingleAsync
        [Fact]
        public async void StrongTypeQuerySingleAsync_ShouldReturnSingleResultAccordingToSql_WithSqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _sqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            connection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = await Queries.Queries.QuerySingleAsync<DailyCustomerPayments>(connection, query, new { PinCode = paramValue });

            //Assert
            Assert.Equal(data.First(x => x.PinCode == paramValue).MobileNumber, result.MobileNumber);
            Assert.IsType<DailyCustomerPayments>(result);

            //Clean
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public async void StrongTypeQuerySingleAsync_ShouldThrowException_WhenZeroElementReturnedFromQery_WithSqlConnection()
        {
            //Arrange
            var connection = _sqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments";

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await Queries.Queries.QuerySingleAsync<DailyCustomerPayments>(connection, query));
        }

        [Fact]
        public async void StrongTypeQuerySingleAsync_ShouldThrowException_WhenMoreThanOneElementsReturnedFromQery_WithMySqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _sqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments";
            connection.BulkInsert(data);

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => Queries.Queries.QuerySingleAsync<DailyCustomerPayments>(connection, query));
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public async void StrongTypeQuerySingleAsync_ShouldReturnSingleResultAccordingToSql_WithNpgSqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _npgSqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            connection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = await Queries.Queries.QuerySingleAsync<DailyCustomerPayments>(connection, query, new { PinCode = paramValue });

            //Assert
            Assert.Equal(data.First(x => x.PinCode == paramValue).MobileNumber, result.MobileNumber);
            Assert.IsType<DailyCustomerPayments>(result);

            //Clean
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public async void StrongTypeQuerySingleAsync_ShouldThrowException_WhenZeroElementReturnedFromQery_WithNpgSqlConnection()
        {
            //Arrange
            var connection = _npgSqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments";

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await Queries.Queries.QuerySingleAsync<DailyCustomerPayments>(connection, query));
        }

        [Fact]
        public async void StrongTypeQuerySingleAsync_ShouldThrowException_WhenMoreThanOneElementsReturnedFromQery_WithNpgMySqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _npgSqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments";
            connection.BulkInsert(data);

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => Queries.Queries.QuerySingleAsync<DailyCustomerPayments>(connection, query));
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public async void StrongTypeQuerySingleAsync_ShouldReturnSingleResultAccordingToSql_WithMySqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _mySqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            connection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = await Queries.Queries.QuerySingleAsync<DailyCustomerPayments>(connection, query, new { PinCode = paramValue });

            //Assert
            Assert.Equal(data.First(x => x.PinCode == paramValue).MobileNumber, result.MobileNumber);
            Assert.IsType<DailyCustomerPayments>(result);

            //Clean
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public async void StrongTypeQuerySingleAsync_ShouldThrowException_WhenZeroElementReturnedFromQery_WithMySqlConnection()
        {
            //Arrange
            var connection = _mySqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments";

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await Queries.Queries.QuerySingleAsync<DailyCustomerPayments>(connection, query));
        }

        [Fact]
        public async void StrongTypeQuerySingleAsync_ShouldThrowException_WhenMoreThanOneElementsReturnedFromQery_WithMyMySqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _mySqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments";
            connection.BulkInsert(data);

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => Queries.Queries.QuerySingleAsync<DailyCustomerPayments>(connection, query));
            connection.Execute(_truncateQuery);
        }
        #endregion

        #region Dynamic QueryFirstAsync
        [Fact]
        public async void QueryFirstAsync_ShouldReturnFirstResultAccordingToSql_WithSqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _sqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            connection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = await Queries.Queries.QueryFirstAsync(connection, query, new { PinCode = paramValue });
            var expected = data.First(x => x.PinCode == paramValue).MobileNumber;
            var actual = result.MobileNumber;

            //Assert
            Assert.Equal(expected, actual);

            //Clean
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public async void QueryFirstAsync_ShouldThrowException_WhenZeroElementReturnedFromQery_WithSqlConnection()
        {
            //Arrange
            var connection = _sqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments";
            connection.Execute(_truncateQuery);

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => Queries.Queries.QueryFirstAsync(connection, query));
        }

        [Fact]
        public async void QueryFirstAsync_ShouldReturnFirstResultAccordingToSql_WithNpgSqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _npgSqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            connection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = await Queries.Queries.QueryFirstAsync(connection, query, new { PinCode = paramValue });
            var expected = data.First(x => x.PinCode == paramValue).MobileNumber;
            var actual = result.mobilenumber;

            //Assert
            Assert.Equal(expected, actual);

            //Clean
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public async void QueryFirstAsync_ShouldThrowException_WhenZeroElementReturnedFromQery_WithNpgSqlConnection()
        {
            //Arrange
            var connection = _npgSqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments";
            connection.Execute(_truncateQuery);

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => Queries.Queries.QueryFirstAsync(connection, query));
        }

        [Fact]
        public async void QueryFirstAsync_ShouldReturnFirstResultAccordingToSql_WithMySqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _mySqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            connection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = await Queries.Queries.QueryFirstAsync(connection, query, new { PinCode = paramValue });
            var expected = data.First(x => x.PinCode == paramValue).MobileNumber;
            var actual = result.MobileNumber;

            //Assert
            Assert.Equal(expected, actual);

            //Clean
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public async void QueryFirstAsync_ShouldThrowException_WhenZeroElementReturnedFromQery_WithMySqlConnection()
        {
            //Arrange
            var connection = _mySqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments";

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => Queries.Queries.QueryFirstAsync(connection, query));
        }
        #endregion

        #region StrongType QueryFirstAsync
        [Fact]
        public async void StrongTypeQueryFirstAsync_ShouldReturnFirstResultAccordingToSql_WithSqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _sqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            connection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = await Queries.Queries.QueryFirstAsync<DailyCustomerPayments>(connection, query, new { PinCode = paramValue });
            var expected = data.First(x => x.PinCode == paramValue).MobileNumber;
            var actual = result.MobileNumber;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsType<DailyCustomerPayments>(result);

            //Clean
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public async void StrongTypeQueryFirstAsync_ShouldThrowException_WhenZeroElementReturnedFromQery_WithSqlConnection()
        {
            //Arrange
            var connection = _sqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments";

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => Queries.Queries.QueryFirstAsync<DailyCustomerPayments>(connection, query));
        }

        [Fact]
        public async void StrongTypeQueryFirstAsync_ShouldReturnFirstResultAccordingToSql_WithNpgSqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _npgSqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            connection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = await Queries.Queries.QueryFirstAsync<DailyCustomerPayments>(connection, query, new { PinCode = paramValue });
            var expected = data.First(x => x.PinCode == paramValue).MobileNumber;
            var actual = result.MobileNumber;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsType<DailyCustomerPayments>(result);

            //Clean
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public async void StrongTypeQueryFirstAsync_ShouldThrowException_WhenZeroElementReturnedFromQery_WithNpgSqlConnection()
        {
            //Arrange
            var connection = _npgSqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments";

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => Queries.Queries.QueryFirstAsync<DailyCustomerPayments>(connection, query));
        }

        [Fact]
        public async void StrongTypeQueryFirstAsync_ShouldReturnFirstResultAccordingToSql_WithMySqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _mySqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            connection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = await Queries.Queries.QueryFirstAsync<DailyCustomerPayments>(connection, query, new { PinCode = paramValue });
            var expected = data.First(x => x.PinCode == paramValue).MobileNumber;
            var actual = result.MobileNumber;
            //Assert
            Assert.Equal(expected, actual);
            Assert.IsType<DailyCustomerPayments>(result);

            //Clean
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public async void StrongTypeQueryFirstAsync_ShouldThrowException_WhenZeroElementReturnedFromQery_WithMySqlConnection()
        {
            //Arrange
            var connection = _mySqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments";
            connection.Execute(_truncateQuery);

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => Queries.Queries.QueryFirstAsync<DailyCustomerPayments>(connection, query));
        }
        #endregion
    }
}
