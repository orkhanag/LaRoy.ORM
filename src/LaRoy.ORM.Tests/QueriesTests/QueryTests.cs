using Dapper;
using LaRoy.ORM.BulkOperations;
using LaRoy.ORM.Tests.DTO;
using Xunit;

namespace LaRoy.ORM.Tests.QueriesTests
{
    public class QueryTests : TestBase
    {
        
        #region Dynamic Query
        [Fact]
        public void Query_ShouldReturnExpectedResultAccordingToSql_WhenSqlConnection_WithoutParameters()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _sqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments";
            connection.BulkInsert(data);

            //Act
            var result = Queries.Queries.Query(connection, query);
            var expected = data.Count();
            var actual = result.Count();

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(expected, actual);

            //Clean
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public void Query_ShouldReturnExpectedResultAccordingToSql_WhenSqlConnection_WithParameters()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _sqlConnection;
            var query = @"SELECT * FROM DailyCustomerPayments WHERE MobileNumber = @MobileNumber";
            connection.BulkInsert(data);

            //Act
            var result = Queries.Queries.Query(connection, query, new { MobileNumber = data.First().MobileNumber });
            var expected = data.First().MobileNumber;
            var actual = result.First().MobileNumber;

            //Assert
            Assert.Single(result);
            Assert.Equal(expected, actual);

            //Clean
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public void Query_ShouldReturnExpectedResultAccordingToSql_WhenNpgSqlConnection_WithoutParameters()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _npgSqlConnection;
            var query = @"SELECT * FROM DailyCustomerPayments";
            connection.BulkInsert(data);

            //Act
            var result = Queries.Queries.Query(connection, query);
            var expected = data.Count();
            var actual = result.Count();

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(expected, actual);

            //Clean
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public void Query_ShouldReturnExpectedResultAccordingToSql_WhenNpgSqlConnection_WithParameters()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _npgSqlConnection;
            var query = @"SELECT * FROM DailyCustomerPayments WHERE MobileNumber = @MobileNumber";
            connection.BulkInsert(data);

            //Act
            var result = Queries.Queries.Query(connection, query, new { MobileNumber = data.First().MobileNumber });
            var expected = data.First().MobileNumber;
            var actual = result.First().mobilenumber;

            //Assert
            Assert.Single(result);
            Assert.Equal(expected, actual);

            //Clean
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public void Query_ShouldReturnExpectedResultAccordingToSql_WhenMySqlConnection_WithoutParameters()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _mySqlConnection;
            var query = @"SELECT * FROM DailyCustomerPayments";
            connection.BulkInsert(data);

            //Act
            var result = Queries.Queries.Query(connection, query);
            var expected = data.Count();
            var actual = result.Count();

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(expected, actual);

            //Clean
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public void Query_ShouldReturnExpectedResultAccordingToSql_WhenMySqlConnection_WithParameters()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _mySqlConnection;
            var query = @"SELECT * FROM DailyCustomerPayments WHERE MobileNumber = @MobileNumber";
            connection.BulkInsert(data);

            //Act
            var result = Queries.Queries.Query(connection, query, new { MobileNumber = data.First().MobileNumber });
            var expected = data.First().MobileNumber;
            var actual = result.First().MobileNumber;

            //Assert
            Assert.Single(result);
            Assert.Equal(expected, actual);

            //Clean
            connection.Execute(_truncateQuery);
        }
        #endregion

        #region StrongType Query
        [Fact]
        public void StrongTypeQuery_ShouldReturnExpectedResultAccordingToSql_WhenSqlConnection_WithoutParameters()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _sqlConnection;
            var query = @"SELECT * FROM DailyCustomerPayments";
            connection.BulkInsert(data);

            //Act
            var result = Queries.Queries.Query<DailyCustomerPayments>(connection, query);
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
        public void StrongTypeQuery_ShouldReturnExpectedResultAccordingToSql_WhenSqlConnection_WithParameters()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _sqlConnection;
            var query = @"SELECT * FROM DailyCustomerPayments WHERE MobileNumber = @MobileNumber";
            connection.BulkInsert(data);

            //Act
            var result = Queries.Queries.Query<DailyCustomerPayments>(connection, query, new { MobileNumber = data.First().MobileNumber });
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
        public void StrongTypeQuery_ShouldReturnExpectedResultAccordingToSql_WhenNpgSqlConnection_WithoutParameters()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _npgSqlConnection;
            var query = @"SELECT * FROM DailyCustomerPayments";
            connection.BulkInsert(data);

            //Act
            var result = Queries.Queries.Query<DailyCustomerPayments>(connection, query);
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
        public void StrongTypeQuery_ShouldReturnExpectedResultAccordingToSql_WhenNpgSqlConnection_WithParameters()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _npgSqlConnection;
            var query = @"SELECT * FROM DailyCustomerPayments WHERE MobileNumber = @MobileNumber";
            connection.BulkInsert(data);

            //Act
            var result = Queries.Queries.Query<DailyCustomerPayments>(connection, query, new { MobileNumber = data.First().MobileNumber });
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
        public void StrongTypeQuery_ShouldReturnExpectedResultAccordingToSql_WhenMySqlConnection_WithoutParameters()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _mySqlConnection;
            var query = @"SELECT * FROM DailyCustomerPayments";
            connection.BulkInsert(data);

            //Act
            var result = Queries.Queries.Query<DailyCustomerPayments>(connection, query);
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
        public void StrongTypeQuery_ShouldReturnExpectedResultAccordingToSql_WhenMySqlConnection_WithParameters()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _mySqlConnection;
            var query = @"SELECT * FROM DailyCustomerPayments WHERE MobileNumber = @MobileNumber";
            connection.BulkInsert(data);

            //Act
            var result = Queries.Queries.Query<DailyCustomerPayments>(connection, query, new { MobileNumber = data.First().MobileNumber });
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

        #region Dynamic QerySingle
        [Fact]
        public void QuerySingle_ShouldReturnSingleResultAccordingToSql_WithSqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _sqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            connection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = Queries.Queries.QuerySingle(connection, query, new { PinCode = paramValue });

            //Assert
            Assert.Equal(data.First(x => x.PinCode == paramValue).MobileNumber, result.MobileNumber);

            //Clean
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public void QuerySingle_ShouldThrowException_WhenZeroElementReturnedFromQery_WithSqlConnection()
        {
            //Arrange
            var data = GenerateTestData(1)!;
            var connection = _sqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            var paramValue = data.First().PinCode;
            connection.Execute(_truncateQuery);

            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => Queries.Queries.QuerySingle(connection, query, new { PinCode = paramValue }));
        }

        [Fact]
        public void QuerySingle_ShouldThrowException_WhenMoreThanOneElementsReturnedFromQery_WithSqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _sqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments";
            connection.BulkInsert(data);

            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => Queries.Queries.QuerySingle(connection, query));
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public void QuerySingle_ShouldReturnSingleResultAccordingToSql_WithNpgSqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _npgSqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            connection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = Queries.Queries.QuerySingle(connection, query, new { PinCode = paramValue });

            //Assert
            Assert.Equal(data.First(x => x.PinCode == paramValue).MobileNumber, result.mobilenumber);

            //Clean
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public void QuerySingle_ShouldThrowException_WhenZeroElementReturnedFromQery_WithNpgSqlConnection()
        {
            //Arrange
            var data = GenerateTestData(1)!;
            var connection = _npgSqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            var paramValue = data.First().PinCode;
            connection.Execute(_truncateQuery);

            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => Queries.Queries.QuerySingle(connection, query, new { PinCode = paramValue }));
        }

        [Fact]
        public void QuerySingle_ShouldThrowException_WhenMoreThanOneElementsReturnedFromQery_WithNpgSqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _npgSqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments";
            connection.BulkInsert(data);

            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => Queries.Queries.QuerySingle(connection, query));
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public void QuerySingle_ShouldReturnSingleResultAccordingToSql_WithMySqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _mySqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            connection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = Queries.Queries.QuerySingle(connection, query, new { PinCode = paramValue });

            //Assert
            Assert.Equal(data.First(x => x.PinCode == paramValue).MobileNumber, result.MobileNumber);

            //Clean
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public void QuerySingle_ShouldThrowException_WhenZeroElementReturnedFromQery_WithMySqlConnection()
        {
            //Arrange
            var data = GenerateTestData(1)!;
            var connection = _mySqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            var paramValue = data.First().PinCode;
            connection.Execute(_truncateQuery);

            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => Queries.Queries.QuerySingle(connection, query, new { PinCode = paramValue }));
        }

        [Fact]
        public void QuerySingle_ShouldThrowException_WhenMoreThanOneElementsReturnedFromQery_WithMySqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _mySqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments";
            connection.BulkInsert(data);

            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => Queries.Queries.QuerySingle(connection, query));
            connection.Execute(_truncateQuery);
        }
        #endregion

        #region StrongType QuerySingle
        [Fact]
        public void StrongTypeQuerySingle_ShouldReturnSingleResultAccordingToSql_WithSqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _sqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            connection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = Queries.Queries.QuerySingle<DailyCustomerPayments>(connection, query, new { PinCode = paramValue });

            //Assert
            Assert.Equal(data.First(x => x.PinCode == paramValue).MobileNumber, result.MobileNumber);
            Assert.IsType<DailyCustomerPayments>(result);

            //Clean
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public void StrongTypeQuerySingle_ShouldThrowException_WhenZeroElementReturnedFromQery_WithSqlConnection()
        {
            //Arrange
            var connection = _sqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments";
            connection.Execute(_truncateQuery);

            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => Queries.Queries.QuerySingle<DailyCustomerPayments>(connection, query));
        }

        [Fact]
        public void StrongTypeQuerySingle_ShouldThrowException_WhenMoreThanOneElementsReturnedFromQery_WithMySqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _sqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments";
            connection.BulkInsert(data);

            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => Queries.Queries.QuerySingle<DailyCustomerPayments>(connection, query));
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public void StrongTypeQuerySingle_ShouldReturnSingleResultAccordingToSql_WithNpgSqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _npgSqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            connection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = Queries.Queries.QuerySingle<DailyCustomerPayments>(connection, query, new { PinCode = paramValue });

            //Assert
            Assert.Equal(data.First(x => x.PinCode == paramValue).MobileNumber, result.MobileNumber);
            Assert.IsType<DailyCustomerPayments>(result);

            //Clean
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public void StrongTypeQuerySingle_ShouldThrowException_WhenZeroElementReturnedFromQery_WithNpgSqlConnection()
        {
            //Arrange
            var connection = _npgSqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments";
            connection.Execute(_truncateQuery);

            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => Queries.Queries.QuerySingle<DailyCustomerPayments>(connection, query));
        }

        [Fact]
        public void StrongTypeQuerySingle_ShouldThrowException_WhenMoreThanOneElementsReturnedFromQery_WithNpgMySqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _npgSqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments";
            connection.BulkInsert(data);

            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => Queries.Queries.QuerySingle<DailyCustomerPayments>(connection, query));
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public void StrongTypeQuerySingle_ShouldReturnSingleResultAccordingToSql_WithMySqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _mySqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            connection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = Queries.Queries.QuerySingle<DailyCustomerPayments>(connection, query, new { PinCode = paramValue });

            //Assert
            Assert.Equal(data.First(x => x.PinCode == paramValue).MobileNumber, result.MobileNumber);
            Assert.IsType<DailyCustomerPayments>(result);

            //Clean
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public void StrongTypeQuerySingle_ShouldThrowException_WhenZeroElementReturnedFromQery_WithMySqlConnection()
        {
            //Arrange
            var connection = _mySqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments";
            connection.Execute(_truncateQuery);

            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => Queries.Queries.QuerySingle<DailyCustomerPayments>(connection, query));
        }

        [Fact]
        public void StrongTypeQuerySingle_ShouldThrowException_WhenMoreThanOneElementsReturnedFromQery_WithMyMySqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _mySqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments";
            connection.BulkInsert(data);

            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => Queries.Queries.QuerySingle<DailyCustomerPayments>(connection, query));
            connection.Execute(_truncateQuery);
        }
        #endregion

        #region Dynamic QueryFirst

        [Fact]
        public void QueryFirst_ShouldReturnFirstResultAccordingToSql_WithSqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _sqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            connection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = Queries.Queries.QueryFirst(connection, query, new { PinCode = paramValue });

            //Assert
            Assert.Equal(data.First(x => x.PinCode == paramValue).MobileNumber, result.MobileNumber);

            //Clean
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public void QueryFirst_ShouldThrowException_WhenZeroElementReturnedFromQery_WithSqlConnection()
        {
            //Arrange
            var connection = _sqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments";
            connection.Execute(_truncateQuery);

            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => Queries.Queries.QueryFirst(connection, query));
        }

        [Fact]
        public void QueryFirst_ShouldReturnFirstResultAccordingToSql_WithNpgSqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _npgSqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            connection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = Queries.Queries.QueryFirst(connection, query, new { PinCode = paramValue });

            //Assert
            Assert.Equal(data.First(x => x.PinCode == paramValue).MobileNumber, result.mobilenumber);

            //Clean
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public void QueryFirst_ShouldThrowException_WhenZeroElementReturnedFromQery_WithNpgSqlConnection()
        {
            //Arrange
            var connection = _npgSqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments";
            connection.Execute(_truncateQuery);

            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => Queries.Queries.QueryFirst(connection, query));
        }

        [Fact]
        public void QueryFirst_ShouldReturnFirstResultAccordingToSql_WithMySqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _mySqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            connection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = Queries.Queries.QueryFirst(connection, query, new { PinCode = paramValue });

            //Assert
            Assert.Equal(data.First(x => x.PinCode == paramValue).MobileNumber, result.MobileNumber);

            //Clean
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public void QueryFirst_ShouldThrowException_WhenZeroElementReturnedFromQery_WithMySqlConnection()
        {
            //Arrange
            var connection = _mySqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments";
            connection.Execute(_truncateQuery);

            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => Queries.Queries.QueryFirst(connection, query));
        }

        #endregion

        #region StrongType QueryFirst
        [Fact]
        public void StrongTypeQueryFirst_ShouldReturnFirstResultAccordingToSql_WithSqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _sqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            connection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = Queries.Queries.QueryFirst<DailyCustomerPayments>(connection, query, new { PinCode = paramValue });

            //Assert
            Assert.Equal(data.First(x => x.PinCode == paramValue).MobileNumber, result.MobileNumber);
            Assert.IsType<DailyCustomerPayments>(result);

            //Clean
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public void StrongTypeQueryFirst_ShouldThrowException_WhenZeroElementReturnedFromQery_WithSqlConnection()
        {
            //Arrange
            var connection = _sqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments";
            connection.Execute(_truncateQuery);

            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => Queries.Queries.QueryFirst<DailyCustomerPayments>(connection, query));
        }

        [Fact]
        public void StrongTypeQueryFirst_ShouldReturnFirstResultAccordingToSql_WithNpgSqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _npgSqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            connection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = Queries.Queries.QueryFirst<DailyCustomerPayments>(connection, query, new { PinCode = paramValue });

            //Assert
            Assert.Equal(data.First(x => x.PinCode == paramValue).MobileNumber, result.MobileNumber);
            Assert.IsType<DailyCustomerPayments>(result);

            //Clean
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public void StrongTypeQueryFirst_ShouldThrowException_WhenZeroElementReturnedFromQery_WithNpgSqlConnection()
        {
            //Arrange
            var connection = _npgSqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments";
            connection.Execute(_truncateQuery);

            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => Queries.Queries.QueryFirst<DailyCustomerPayments>(connection, query));
        }

        [Fact]
        public void StrongTypeQueryFirst_ShouldReturnFirstResultAccordingToSql_WithMySqlConnection()
        {
            //Arrange
            var data = GenerateTestData(10)!;
            var connection = _mySqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments WHERE PinCode = @PinCode";
            connection.BulkInsert(data);
            var paramValue = data.First().PinCode;

            //Act
            var result = Queries.Queries.QueryFirst<DailyCustomerPayments>(connection, query, new { PinCode = paramValue });

            //Assert
            Assert.Equal(data.First(x => x.PinCode == paramValue).MobileNumber, result.MobileNumber);
            Assert.IsType<DailyCustomerPayments>(result);

            //Clean
            connection.Execute(_truncateQuery);
        }

        [Fact]
        public void StrongTypeQueryFirst_ShouldThrowException_WhenZeroElementReturnedFromQery_WithMySqlConnection()
        {
            //Arrange
            var connection = _mySqlConnection;
            var query = "SELECT * FROM DailyCustomerPayments";
            connection.Execute(_truncateQuery);

            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => Queries.Queries.QueryFirst<DailyCustomerPayments>(connection, query));
        }
        #endregion

        #region Dynamic QuerySingleOrDefault
        [Fact]
        public void QuerySingleOrDefault_ShouldReturnNull_WhenQueryReturnsZeroElement_WithSqlConnection()
        {
            //Arrange
            var query = "SELECT * FROM DailyCustomerPayments";
            var connection = _sqlConnection;
            connection.Execute(_truncateQuery);

            //Act
            var result = Queries.Queries.QuerySingleOrDefault(connection, query);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void QuerySingleOrDefault_ShouldReturnNull_WhenQueryReturnsZeroElement_WithNpgSqlConnection()
        {
            //Arrange
            var query = "SELECT * FROM DailyCustomerPayments";
            var connection = _npgSqlConnection;
            connection.Execute(_truncateQuery);

            //Act
            var result = Queries.Queries.QuerySingleOrDefault(connection, query);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void QuerySingleOrDefault_ShouldReturnNull_WhenQueryReturnsZeroElement_WithMySqlConnection()
        {
            //Arrange
            var query = "SELECT * FROM DailyCustomerPayments";
            var connection = _mySqlConnection;
            connection.Execute(_truncateQuery);

            //Act
            var result = Queries.Queries.QuerySingleOrDefault(connection, query);

            //Assert
            Assert.Null(result);
        }
        #endregion

        #region StrongType QuerySingleOrDefault
        [Fact]
        public void StrongTypeQuerySingleOrDefault_ShouldReturnDefaultValueOfGivenType_WhenQueryReturnsZeroElement_WithSqlConnection()
        {
            //Arrange
            var connection = _sqlConnection;
            var query = "SELECT COUNT(*) FROM DailyCustomerPayments";
            connection.Execute(_truncateQuery);

            //Act
            var result = Queries.Queries.QuerySingleOrDefault<int>(connection, query);

            //Assert
            Assert.Equal(default, result);
        }

        [Fact]
        public void StrongTypeQuerySingleOrDefault_ShouldReturnDefaultValueOfGivenType_WhenQueryReturnsZeroElement_WithNpgSqlConnection()
        {
            //Arrange
            var connection = _npgSqlConnection;
            var query = "SELECT COUNT(*) FROM DailyCustomerPayments";
            connection.Execute(_truncateQuery);

            //Act
            var result = Queries.Queries.QuerySingleOrDefault<int>(connection, query);

            //Assert
            Assert.Equal(default, result);
        }

        [Fact]
        public void StrongTypeQuerySingleOrDefault_ShouldReturnDefaultValueOfGivenType_WhenQueryReturnsZeroElement_WithMySqlConnection()
        {
            //Arrange
            var connection = _mySqlConnection;
            var query = "SELECT COUNT(*) FROM DailyCustomerPayments";
            connection.Execute(_truncateQuery);

            //Act
            var result = Queries.Queries.QuerySingleOrDefault<int>(connection, query);

            //Assert
            Assert.Equal(default, result);
        }
        #endregion

        #region Dynamic QueryFirstOrDefault
        [Fact]
        public void QueryFirstOrDefault_ShouldReturnNull_WhenQueryReturnsZeroElement_WithSqlConnection()
        {
            //Arrange
            var query = "SELECT * FROM DailyCustomerPayments";
            var connection = _sqlConnection;
            connection.Execute(_truncateQuery);

            //Act
            var result = Queries.Queries.QueryFirstOrDefault(connection, query);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void QueryFirstOrDefault_ShouldReturnNull_WhenQueryReturnsZeroElement_WithNpgSqlConnection()
        {
            //Arrange
            var query = "SELECT * FROM DailyCustomerPayments";
            var connection = _npgSqlConnection;
            connection.Execute(_truncateQuery);

            //Act
            var result = Queries.Queries.QueryFirstOrDefault(connection, query);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void QueryFirstOrDefault_ShouldReturnNull_WhenQueryReturnsZeroElement_WithMySqlConnection()
        {
            //Arrange
            var query = "SELECT * FROM DailyCustomerPayments";
            var connection = _mySqlConnection;
            connection.Execute(_truncateQuery);

            //Act
            var result = Queries.Queries.QueryFirstOrDefault(connection, query);

            //Assert
            Assert.Null(result);
        }
        #endregion

        #region StrongType QueryFirstOrDefault
        [Fact]
        public void StrongTypeQueryFirstOrDefault_ShouldReturnDefaultValueOfGivenType_WhenQueryReturnsZeroElement_WithSqlConnection()
        {
            //Arrange
            var query = "SELECT COUNT(*) FROM DailyCustomerPayments";
            var connection = _sqlConnection;
            connection.Execute(_truncateQuery);

            //Act
            var result = Queries.Queries.QueryFirstOrDefault<int>(connection, query);

            //Assert
            Assert.Equal(default, result);
        }

        [Fact]
        public void StrongTypeQueryFirstOrDefault_ShouldReturnDefaultValueOfGivenType_WhenQueryReturnsZeroElement_WithNpgSqlConnection()
        {
            //Arrange
            var query = "SELECT COUNT(*) FROM DailyCustomerPayments";
            var connection = _npgSqlConnection;
            connection.Execute(_truncateQuery);

            //Act
            var result = Queries.Queries.QueryFirstOrDefault<int>(connection, query);

            //Assert
            Assert.Equal(default, result);
        }

        [Fact]
        public void StrongTypeQueryFirstOrDefault_ShouldReturnDefaultValueOfGivenType_WhenQueryReturnsZeroElement_WithMySqlConnection()
        {
            //Arrange
            var query = "SELECT COUNT(*) FROM DailyCustomerPayments";
            var connection = _mySqlConnection;
            connection.Execute(_truncateQuery);

            //Act
            var result = Queries.Queries.QueryFirstOrDefault<int>(connection, query);

            //Assert
            Assert.Equal(default, result);
        }
        #endregion 
    }
}
