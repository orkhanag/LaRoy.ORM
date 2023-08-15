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
            var query = "SELECT * FROM DailyCustomerPayments";
            _sqlConnection.BulkInsert(data);

            //Act
            var result = await Queries.Queries.QueryAsync(_sqlConnection, query);
            var expected = data.Count();
            var actual = result.Count();

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(expected, actual);

            //Clean
            _sqlConnection.Execute(_truncateQuery);
        }
        [Fact]
        public async void QueryAsync_ShouldReturnExpectedResultAccordingToSql_WhenSqlConnection_WithParameters()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var query = @"SELECT * FROM DailyCustomerPayments WHERE MobileNumber = @MobileNumber";
            _sqlConnection.BulkInsert(data);

            //Act
            var result = await Queries.Queries.QueryAsync(_sqlConnection, query, new { MobileNumber = data.First().MobileNumber });
            var expected = data.First().MobileNumber;
            var actual = result.First().MobileNumber;

            //Assert
            Assert.Single(result);
            Assert.Equal(expected, actual);

            //Clean
            _sqlConnection.Execute(_truncateQuery);
        }
        [Fact]
        public async void QueryAsync_ShouldReturnExpectedResultAccordingToSql_WhenNpgSqlConnection_WithoutParameters()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var query = @"SELECT * FROM DailyCustomerPayments";
            _npgSqlConnection.BulkInsert(data);

            //Act
            var result = await Queries.Queries.QueryAsync(_npgSqlConnection, query);
            var expected = data.Count();
            var actual = result.Count();

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(expected, actual);

            //Clean
            _npgSqlConnection.Execute(_truncateQuery);
        }
        [Fact]
        public async void QueryAsync_ShouldReturnExpectedResultAccordingToSql_WhenNpgSqlConnection_WithParameters()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var query = @"SELECT * FROM DailyCustomerPayments WHERE MobileNumber = @MobileNumber";
            _npgSqlConnection.BulkInsert(data);

            //Act
            var result = await Queries.Queries.QueryAsync(_npgSqlConnection, query, new { data.First().MobileNumber });
            var expected = data.First().MobileNumber;
            var actual = result.First().mobilenumber;

            //Assert
            Assert.Single(result);
            Assert.Equal(expected, actual);

            //Clean
            _npgSqlConnection.Execute(_truncateQuery);
        }
        [Fact]
        public async void QueryAsync_ShouldReturnExpectedResultAccordingToSql_WhenMySqlConnection_WithoutParameters()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var query = @"SELECT * FROM DailyCustomerPayments";
            _mySqlConnection.BulkInsert(data);

            //Act
            var result = await Queries.Queries.QueryAsync(_mySqlConnection, query);
            var expected = data.Count();
            var actual = result.Count();

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(expected, actual);

            //Clean
            _mySqlConnection.Execute(_truncateQuery);
        }
        [Fact]
        public async void QueryAsync_ShouldReturnExpectedResultAccordingToSql_WhenMySqlConnection_WithParameters()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var query = @"SELECT * FROM DailyCustomerPayments WHERE MobileNumber = @MobileNumber";
            _mySqlConnection.BulkInsert(data);

            //Act
            var result = await Queries.Queries.QueryAsync(_mySqlConnection, query, new { data.First().MobileNumber });
            var expected = data.First().MobileNumber;
            var actual = result.First().MobileNumber;

            //Assert
            Assert.Single(result);
            Assert.Equal(expected, actual);

            //Clean
            _mySqlConnection.Execute(_truncateQuery);
        }
        #endregion

        #region StrongType QueryAsync
        [Fact]
        public async void StrongTypeQueryAsync_ShouldReturnExpectedResultAccordingToSql_WhenSqlConnection_WithoutParameters()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var query = @"SELECT * FROM DailyCustomerPayments";
            _sqlConnection.BulkInsert(data);

            //Act
            var result = await Queries.Queries.QueryAsync<DailyCustomerPayments>(_sqlConnection, query);
            var expected = data.Count();
            var actual = result.Count();
            var type = result.First();

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(expected, actual);
            Assert.IsType<DailyCustomerPayments>(type);

            //Clean
            _sqlConnection.Execute(_truncateQuery);
        }

        [Fact]
        public async void StrongTypeQueryAsync_ShouldReturnExpectedResultAccordingToSql_WhenSqlConnection_WithParameters()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var query = @"SELECT * FROM DailyCustomerPayments WHERE MobileNumber = @MobileNumber";
            _sqlConnection.BulkInsert(data);

            //Act
            var result = await Queries.Queries.QueryAsync<DailyCustomerPayments>(_sqlConnection, query, new { data.First().MobileNumber });
            var expected = data.First().MobileNumber;
            var actual = result.First().MobileNumber;
            var type = result.First();

            //Assert
            Assert.Single(result);
            Assert.Equal(expected, actual);
            Assert.IsType<DailyCustomerPayments>(type);

            //Clean
            _sqlConnection.Execute(_truncateQuery);
        }

        [Fact]
        public async void StrongTypeQueryAsync_ShouldReturnExpectedResultAccordingToSql_WhenNpgSqlConnection_WithoutParameters()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var query = @"SELECT * FROM DailyCustomerPayments";
            _npgSqlConnection.BulkInsert(data);

            //Act
            var result = await Queries.Queries.QueryAsync<DailyCustomerPayments>(_npgSqlConnection, query);
            var expected = data.Count();
            var actual = result.Count();
            var type = result.First();

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(expected, actual);
            Assert.IsType<DailyCustomerPayments>(type);

            //Clean
            _npgSqlConnection.Execute(_truncateQuery);
        }

        [Fact]
        public async void StrongTypeQueryAsync_ShouldReturnExpectedResultAccordingToSql_WhenNpgSqlConnection_WithParameters()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var query = @"SELECT * FROM DailyCustomerPayments WHERE MobileNumber = @MobileNumber";
            _npgSqlConnection.BulkInsert(data);

            //Act
            var result = await Queries.Queries.QueryAsync<DailyCustomerPayments>(_npgSqlConnection, query, new { data.First().MobileNumber });
            var expected = data.First().MobileNumber;
            var actual = result.First().MobileNumber;
            var type = result.First();

            //Assert
            Assert.Single(result);
            Assert.Equal(expected, actual);
            Assert.IsType<DailyCustomerPayments>(type);
            //Clean
            _npgSqlConnection.Execute(_truncateQuery);
        }

        [Fact]
        public async void StrongTypeQueryAsync_ShouldReturnExpectedResultAccordingToSql_WhenMySqlConnection_WithoutParameters()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var query = @"SELECT * FROM DailyCustomerPayments";
            _mySqlConnection.BulkInsert(data);

            //Act
            var result = await Queries.Queries.QueryAsync<DailyCustomerPayments>(_mySqlConnection, query);
            var expected = data.Count();
            var actual = result.Count();
            var type = result.FirstOrDefault();

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(expected, actual);
            Assert.IsType<DailyCustomerPayments>(type);

            //Clean
            _mySqlConnection.Execute(_truncateQuery);
        }

        [Fact]
        public async void StrongTypeQueryAsync_ShouldReturnExpectedResultAccordingToSql_WhenMySqlConnection_WithParameters()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var query = @"SELECT * FROM DailyCustomerPayments WHERE MobileNumber = @MobileNumber";
            _mySqlConnection.BulkInsert(data);

            //Act
            var result = await Queries.Queries.QueryAsync<DailyCustomerPayments>(_mySqlConnection, query, new { data.First().MobileNumber });
            var expected = data.First().MobileNumber;
            var actual = result.First().MobileNumber;
            var type = result.FirstOrDefault();

            //Assert
            Assert.Single(result);
            Assert.Equal(expected, actual);
            Assert.IsType<DailyCustomerPayments>(type);

            //Clean
            _mySqlConnection.Execute(_truncateQuery);
        }
        #endregion

        #region Dynamic QerySingleAsync
        [Fact]
        public async void QuerySingleAsync_ShouldReturnSingleResultAccordingToSql_WithSqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            _sqlConnection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = await Queries.Queries.QuerySingleAsync(_sqlConnection, query, new { PinCode = paramValue });
            var actual = data.First(x => x.PinCode == paramValue).MobileNumber;
            var expected = result.MobileNumber;

            //Assert
            Assert.Equal(expected, actual);

            //Clean
            _sqlConnection.Execute(_truncateQuery);
        }

        [Fact]
        public async void QuerySingleAsync_ShouldThrowException_WhenZeroElementReturnedFromQery_WithSqlConnection()
        {
            //Arrange
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            var paramValue = "1Q2W3E4";

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => Queries.Queries.QuerySingleAsync(_sqlConnection, query, new { PinCode = paramValue }));
        }

        [Fact]
        public async void QuerySingleAsync_ShouldThrowException_WhenMoreThanOneElementsReturnedFromQery_WithSqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var query = "SELECT * FROM DailyCustomerPayments";
            _sqlConnection.BulkInsert(data);

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => Queries.Queries.QuerySingleAsync(_sqlConnection, query));
            _sqlConnection.Execute(_truncateQuery);
        }

        [Fact]
        public async void QuerySingleAsync_ShouldReturnSingleResultAccordingToSql_WithNpgSqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            _npgSqlConnection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = await Queries.Queries.QuerySingleAsync(_npgSqlConnection, query, new { PinCode = paramValue });

            //Assert
            Assert.Equal(data.First(x => x.PinCode == paramValue).MobileNumber, result.mobilenumber);

            //Clean
            _npgSqlConnection.Execute(_truncateQuery);
        }

        [Fact]
        public async void QuerySingleAsync_ShouldThrowException_WhenZeroElementReturnedFromQery_WithNpgSqlConnection()
        {
            //Arrange
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            var paramValue = "1Q2W3E4";

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => Queries.Queries.QuerySingleAsync(_npgSqlConnection, query, new { PinCode = paramValue }));
        }

        [Fact]
        public async void QuerySingleAsync_ShouldThrowException_WhenMoreThanOneElementsReturnedFromQery_WithNpgSqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var query = "SELECT * FROM DailyCustomerPayments";
            _npgSqlConnection.BulkInsert(data);

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => Queries.Queries.QuerySingleAsync(_npgSqlConnection, query));
            _npgSqlConnection.Execute(_truncateQuery);
        }

        [Fact]
        public async void QuerySingleAsync_ShouldReturnSingleResultAccordingToSql_WithMySqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            _mySqlConnection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = await Queries.Queries.QuerySingleAsync(_mySqlConnection, query, new { PinCode = paramValue });

            //Assert
            Assert.Equal(data.First(x => x.PinCode == paramValue).MobileNumber, result.MobileNumber);

            //Clean
            _mySqlConnection.Execute(_truncateQuery);
        }

        [Fact]
        public async void QuerySingleAsync_ShouldThrowException_WhenZeroElementReturnedFromQery_WithMySqlConnection()
        {
            //Arrange
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            var paramValue = "1Q2W3E4";

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => Queries.Queries.QuerySingleAsync(_mySqlConnection, query, new { PinCode = paramValue }));
        }

        [Fact]
        public async void QuerySingleAsync_ShouldThrowException_WhenMoreThanOneElementsReturnedFromQery_WithMySqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var query = "SELECT * FROM DailyCustomerPayments";
            _mySqlConnection.BulkInsert(data);

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => Queries.Queries.QuerySingleAsync(_mySqlConnection, query));
            _mySqlConnection.Execute(_truncateQuery);
        }
        #endregion

        #region StrongType QuerySingleAsync
        [Fact]
        public async void StrongTypeQuerySingleAsync_ShouldReturnSingleResultAccordingToSql_WithSqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            _sqlConnection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = await Queries.Queries.QuerySingleAsync<DailyCustomerPayments>(_sqlConnection, query, new { PinCode = paramValue });

            //Assert
            Assert.Equal(data.First(x => x.PinCode == paramValue).MobileNumber, result.MobileNumber);
            Assert.IsType<DailyCustomerPayments>(result);

            //Clean
            _sqlConnection.Execute(_truncateQuery);
        }

        [Fact]
        public async void StrongTypeQuerySingleAsync_ShouldThrowException_WhenZeroElementReturnedFromQery_WithSqlConnection()
        {
            //Arrange
            var query = "SELECT * FROM DailyCustomerPayments";

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await Queries.Queries.QuerySingleAsync<DailyCustomerPayments>(_sqlConnection, query));
        }

        [Fact]
        public async void StrongTypeQuerySingleAsync_ShouldThrowException_WhenMoreThanOneElementsReturnedFromQery_WithMySqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var query = "SELECT * FROM DailyCustomerPayments";
            _sqlConnection.BulkInsert(data);

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => Queries.Queries.QuerySingleAsync<DailyCustomerPayments>(_sqlConnection, query));
            _sqlConnection.Execute(_truncateQuery);
        }

        [Fact]
        public async void StrongTypeQuerySingleAsync_ShouldReturnSingleResultAccordingToSql_WithNpgSqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            _npgSqlConnection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = await Queries.Queries.QuerySingleAsync<DailyCustomerPayments>(_npgSqlConnection, query, new { PinCode = paramValue });

            //Assert
            Assert.Equal(data.First(x => x.PinCode == paramValue).MobileNumber, result.MobileNumber);
            Assert.IsType<DailyCustomerPayments>(result);

            //Clean
            _npgSqlConnection.Execute(_truncateQuery);
        }

        [Fact]
        public async void StrongTypeQuerySingleAsync_ShouldThrowException_WhenZeroElementReturnedFromQery_WithNpgSqlConnection()
        {
            //Arrange
            var query = "SELECT * FROM DailyCustomerPayments";

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await Queries.Queries.QuerySingleAsync<DailyCustomerPayments>(_npgSqlConnection, query));
        }

        [Fact]
        public async void StrongTypeQuerySingleAsync_ShouldThrowException_WhenMoreThanOneElementsReturnedFromQery_WithNpgMySqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var query = "SELECT * FROM DailyCustomerPayments";
            _npgSqlConnection.BulkInsert(data);

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => Queries.Queries.QuerySingleAsync<DailyCustomerPayments>(_npgSqlConnection, query));
            _npgSqlConnection.Execute(_truncateQuery);
        }

        [Fact]
        public async void StrongTypeQuerySingleAsync_ShouldReturnSingleResultAccordingToSql_WithMySqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            _mySqlConnection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = await Queries.Queries.QuerySingleAsync<DailyCustomerPayments>(_mySqlConnection, query, new { PinCode = paramValue });

            //Assert
            Assert.Equal(data.First(x => x.PinCode == paramValue).MobileNumber, result.MobileNumber);
            Assert.IsType<DailyCustomerPayments>(result);

            //Clean
            _mySqlConnection.Execute(_truncateQuery);
        }

        [Fact]
        public async void StrongTypeQuerySingleAsync_ShouldThrowException_WhenZeroElementReturnedFromQery_WithMySqlConnection()
        {
            //Arrange
            var query = "SELECT * FROM DailyCustomerPayments";

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await Queries.Queries.QuerySingleAsync<DailyCustomerPayments>(_mySqlConnection, query));
        }

        [Fact]
        public async void StrongTypeQuerySingleAsync_ShouldThrowException_WhenMoreThanOneElementsReturnedFromQery_WithMyMySqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var query = "SELECT * FROM DailyCustomerPayments";
            _mySqlConnection.BulkInsert(data);

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => Queries.Queries.QuerySingleAsync<DailyCustomerPayments>(_mySqlConnection, query));
            _mySqlConnection.Execute(_truncateQuery);
        }
        #endregion

        #region Dynamic QueryFirstAsync
        [Fact]
        public async void QueryFirstAsync_ShouldReturnFirstResultAccordingToSql_WithSqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            _sqlConnection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = await Queries.Queries.QueryFirstAsync(_sqlConnection, query, new { PinCode = paramValue });
            var expected = data.First(x => x.PinCode == paramValue).MobileNumber;
            var actual = result.MobileNumber;

            //Assert
            Assert.Equal(expected, actual);

            //Clean
            _sqlConnection.Execute(_truncateQuery);
        }

        [Fact]
        public async void QueryFirstAsync_ShouldThrowException_WhenZeroElementReturnedFromQery_WithSqlConnection()
        {
            //Arrange
            var query = "SELECT * FROM DailyCustomerPayments";
            _sqlConnection.Execute(_truncateQuery);

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => Queries.Queries.QueryFirstAsync(_sqlConnection, query));
        }

        [Fact]
        public async void QueryFirstAsync_ShouldReturnFirstResultAccordingToSql_WithNpgSqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            _npgSqlConnection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = await Queries.Queries.QueryFirstAsync(_npgSqlConnection, query, new { PinCode = paramValue });
            var expected = data.First(x => x.PinCode == paramValue).MobileNumber;
            var actual = result.mobilenumber;

            //Assert
            Assert.Equal(expected, actual);

            //Clean
            _npgSqlConnection.Execute(_truncateQuery);
        }

        [Fact]
        public async void QueryFirstAsync_ShouldThrowException_WhenZeroElementReturnedFromQery_WithNpgSqlConnection()
        {
            //Arrange
            var query = "SELECT * FROM DailyCustomerPayments";
            _npgSqlConnection.Execute(_truncateQuery);

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => Queries.Queries.QueryFirstAsync(_npgSqlConnection, query));
        }

        [Fact]
        public async void QueryFirstAsync_ShouldReturnFirstResultAccordingToSql_WithMySqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            _mySqlConnection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = await Queries.Queries.QueryFirstAsync(_mySqlConnection, query, new { PinCode = paramValue });
            var expected = data.First(x => x.PinCode == paramValue).MobileNumber;
            var actual = result.MobileNumber;

            //Assert
            Assert.Equal(expected, actual);

            //Clean
            _mySqlConnection.Execute(_truncateQuery);
        }

        [Fact]
        public async void QueryFirstAsync_ShouldThrowException_WhenZeroElementReturnedFromQery_WithMySqlConnection()
        {
            //Arrange
            var query = "SELECT * FROM DailyCustomerPayments";

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => Queries.Queries.QueryFirstAsync(_mySqlConnection, query));
        }
        #endregion

        #region StrongType QueryFirstAsync
        [Fact]
        public async void StrongTypeQueryFirstAsync_ShouldReturnFirstResultAccordingToSql_WithSqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            _sqlConnection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = await Queries.Queries.QueryFirstAsync<DailyCustomerPayments>(_sqlConnection, query, new { PinCode = paramValue });
            var expected = data.First(x => x.PinCode == paramValue).MobileNumber;
            var actual = result.MobileNumber;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsType<DailyCustomerPayments>(result);

            //Clean
            _sqlConnection.Execute(_truncateQuery);
        }

        [Fact]
        public async void StrongTypeQueryFirstAsync_ShouldThrowException_WhenZeroElementReturnedFromQery_WithSqlConnection()
        {
            //Arrange
            var query = "SELECT * FROM DailyCustomerPayments";

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => Queries.Queries.QueryFirstAsync<DailyCustomerPayments>(_sqlConnection, query));
        }

        [Fact]
        public async void StrongTypeQueryFirstAsync_ShouldReturnFirstResultAccordingToSql_WithNpgSqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            _npgSqlConnection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = await Queries.Queries.QueryFirstAsync<DailyCustomerPayments>(_npgSqlConnection, query, new { PinCode = paramValue });
            var expected = data.First(x => x.PinCode == paramValue).MobileNumber;
            var actual = result.MobileNumber;

            //Assert
            Assert.Equal(expected, actual);
            Assert.IsType<DailyCustomerPayments>(result);

            //Clean
            _npgSqlConnection.Execute(_truncateQuery);
        }

        [Fact]
        public async void StrongTypeQueryFirstAsync_ShouldThrowException_WhenZeroElementReturnedFromQery_WithNpgSqlConnection()
        {
            //Arrange
            var query = "SELECT * FROM DailyCustomerPayments";

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => Queries.Queries.QueryFirstAsync<DailyCustomerPayments>(_npgSqlConnection, query));
        }

        [Fact]
        public async void StrongTypeQueryFirstAsync_ShouldReturnFirstResultAccordingToSql_WithMySqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            _mySqlConnection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = await Queries.Queries.QueryFirstAsync<DailyCustomerPayments>(_mySqlConnection, query, new { PinCode = paramValue });
            var expected = data.First(x => x.PinCode == paramValue).MobileNumber;
            var actual = result.MobileNumber;
            //Assert
            Assert.Equal(expected, actual);
            Assert.IsType<DailyCustomerPayments>(result);

            //Clean
            _mySqlConnection.Execute(_truncateQuery);
        }

        [Fact]
        public async void StrongTypeQueryFirstAsync_ShouldThrowException_WhenZeroElementReturnedFromQery_WithMySqlConnection()
        {
            //Arrange
            var query = "SELECT * FROM DailyCustomerPayments";

            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => Queries.Queries.QueryFirstAsync<DailyCustomerPayments>(_mySqlConnection, query));
        }
        #endregion

        #region Dynamic QuerySingleOrDefaultAsync
        [Fact]
        public async void QuerySingleOrDefaultAsync_ShouldReturnNull_WhenQueryReturnsZeroElement_WithSqlConnection()
        {
            //Arrange
            var query = "SELECT * FROM DailyCustomerPayments";

            //Act
            var result = await Queries.Queries.QuerySingleOrDefaultAsync(_sqlConnection, query);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void QuerySingleOrDefaultAsync_ShouldReturnNull_WhenQueryReturnsZeroElement_WithNpgSqlConnection()
        {
            //Arrange
            var query = "SELECT * FROM DailyCustomerPayments";

            //Act
            var result = await Queries.Queries.QuerySingleOrDefaultAsync(_npgSqlConnection, query);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void QuerySingleOrDefaultAsync_ShouldReturnNull_WhenQueryReturnsZeroElement_WithMySqlConnection()
        {
            //Arrange
            var query = "SELECT * FROM DailyCustomerPayments";

            //Act
            var result = await Queries.Queries.QuerySingleOrDefaultAsync(_mySqlConnection, query);

            //Assert
            Assert.Null(result);
        }
        #endregion

        #region StrongType QuerySingleOrDefaultAsync
        [Fact]
        public async void StrongTypeQuerySingleOrDefaultAsync_ShouldReturnDefaultValueOfGivenType_WhenQueryReturnsZeroElement_WithSqlConnection()
        {
            //Arrange
            var query = "SELECT * FROM DailyCustomerPayments";

            //Act
            var result = await Queries.Queries.QuerySingleOrDefaultAsync<DailyCustomerPayments>(_sqlConnection, query);

            //Assert
            Assert.Equal(default, result);
        }

        [Fact]
        public async void StrongTypeQuerySingleOrDefaultAsync_ShouldReturnDefaultValueOfGivenType_WhenQueryReturnsZeroElement_WithNpgSqlConnection()
        {
            //Arrange
            var query = "SELECT * FROM DailyCustomerPayments";

            //Act
            var result = await Queries.Queries.QuerySingleOrDefaultAsync<DailyCustomerPayments>(_npgSqlConnection, query);

            //Assert
            Assert.Equal(default, result);
        }

        [Fact]
        public async void StrongTypeQuerySingleOrDefaultAsync_ShouldReturnDefaultValueOfGivenType_WhenQueryReturnsZeroElement_WithMySqlConnection()
        {
            //Arrange
            var query = "SELECT * FROM DailyCustomerPayments";

            //Act
            var result = await Queries.Queries.QuerySingleOrDefaultAsync<DailyCustomerPayments>(_mySqlConnection, query);

            //Assert
            Assert.Equal(default, result);
        }
        #endregion

        #region Dynamic QueryFirstOrDefaultAsync
        [Fact]
        public async void QueryFirstOrDefaultAsync_ShouldReturnNull_WhenQueryReturnsZeroElement_WithSqlConnection()
        {
            //Arrange
            var query = "SELECT * FROM DailyCustomerPayments";

            //Act
            var result = await Queries.Queries.QueryFirstOrDefaultAsync(_sqlConnection, query);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void QueryFirstOrDefaultAsync_ShouldReturnNull_WhenQueryReturnsZeroElement_WithNpgSqlConnection()
        {
            //Arrange
            var query = "SELECT * FROM DailyCustomerPayments";

            //Act
            var result = await Queries.Queries.QueryFirstOrDefaultAsync(_npgSqlConnection, query);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void QueryFirstOrDefaultAsync_ShouldReturnNull_WhenQueryReturnsZeroElement_WithMySqlConnection()
        {
            //Arrange
            var query = "SELECT * FROM DailyCustomerPayments";

            //Act
            var result = await Queries.Queries.QueryFirstOrDefaultAsync(_mySqlConnection, query);

            //Assert
            Assert.Null(result);
        }
        #endregion

        #region StrongType QueryFirstOrDefaultAsync
        [Fact]
        public async void StrongTypeQueryFirstOrDefault_ShouldReturnDefaultValueOfGivenType_WhenQueryReturnsZeroElement_WithSqlConnection()
        {
            //Arrange
            var query = "SELECT COUNT(*) FROM DailyCustomerPayments";

            //Act
            var result = await Queries.Queries.QueryFirstOrDefaultAsync<int>(_sqlConnection, query);

            //Assert
            Assert.Equal(default, result);
        }

        [Fact]
        public async void StrongTypeQueryFirstOrDefaultAsync_ShouldReturnDefaultValueOfGivenType_WhenQueryReturnsZeroElement_WithNpgSqlConnection()
        {
            //Arrange
            var query = "SELECT COUNT(*) FROM DailyCustomerPayments";

            //Act
            var result = await Queries.Queries.QueryFirstOrDefaultAsync<int>(_npgSqlConnection, query);

            //Assert
            Assert.Equal(default, result);
        }

        [Fact]
        public async void StrongTypeQueryFirstOrDefaultAsync_ShouldReturnDefaultValueOfGivenType_WhenQueryReturnsZeroElement_WithMySqlConnection()
        {
            //Arrange
            var query = "SELECT COUNT(*) FROM DailyCustomerPayments";

            //Act
            var result = await Queries.Queries.QueryFirstOrDefaultAsync<int>(_mySqlConnection, query);

            //Assert
            Assert.Equal(default, result);
        }
        #endregion 
    }
}
