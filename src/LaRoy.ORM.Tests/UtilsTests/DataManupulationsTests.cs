using LaRoy.ORM.Tests.DTO;
using LaRoy.ORM.Utils;
using System.Data;
using Xunit;

namespace LaRoy.ORM.Tests.UtilsTests
{
    public class DataManupulationsTests : TestBase
    {
        [Theory]
        [InlineData(1)]
        [InlineData(69)]
        [InlineData(420)]
        [InlineData(4269)]
        public void ToDataTable_ShouldConvertDataToDataTable(int dataCount)
        {
            //Arrange
            var data = GenerateTestData(dataCount);
            //Act
            DataTable dataTable = data.ToDataTable();
            //Assert
            var columnNames = dataTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList();
            var dataType = data.First().GetType();

            Assert.Equal(dataType.Name, dataTable.TableName);
            Assert.Equal(data.Count(), dataTable.Rows.Count);
            Assert.Equal(dataType.GetProperties().Length, dataTable.Columns.Count);

            foreach (var item in dataType.GetProperties())
                Assert.Contains(item.Name, columnNames);
        }

        [Fact]
        public void GetKeyField_ShouldGetFiledWithKeyAttribute()
        {
            //Arrange
            var item = new DailyCustomerPayments();
            //Act
            var keyField = DataManupulations.GetKeyField<DailyCustomerPayments>();
            //
            Assert.Equal("PinCode", keyField.Name);
            Assert.Equal(typeof(string), keyField.PropertyType);
        }

        [Fact]
        public void GetKeyField_ShouldThrowNotSupportedException_WhenTypeHasNoKeyField()
        {
            // Arrange
            var instance = new TypeWithoutKeyField();

            // Act & Assert
            Assert.Throws<NotSupportedException>(() => DataManupulations.GetKeyField<TypeWithoutKeyField>());
        }
    }
}
