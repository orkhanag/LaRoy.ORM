# LaRoy ORM

> An open-source free ORM for .NET to bring the best features of the most used ORMs, into one place

## Install

1. .NET CLI

```
dotnet add package LaRoy.ORM --version 1.0.5-beta
```

2. NuGet Package Manager

```
NuGet\Install-Package LaRoy.ORM -Version 1.0.5-beta
```

3. Paket CLI

```
paket add LaRoy.ORM --version 1.0.5-beta
```

## How to use

### Create a database connection(For now only MsSQL, PostgreSQL and MySQL supported)

```cs
var connection = new SqlConnection("your connection string")

var connection = new NpgSqlConnection("your connection string")

var connection = new MySqlConnection("your connection string")
```

  Or use LaRoyDbContext abstract class to create your DbContext

```cs
    public class SqlDbContext : LaRoyDbContext
    {
        public override DbConnection CreateConnection()
            => new SqlConnection("your connection string");
    }

    public class NpgSqlDbContext : LaRoyDbContext
    {
        public override DbConnection CreateConnection()
            => new NpgsqlConnection("your connection string");
    }

    public class MySqlDbContext : LaRoyDbContext
    {
        public override DbConnection CreateConnection()
            => new MySqlConnection("your connection string");
    }
```

### Bulk operations
1. Bulk Insert
```cs
//Arrange IEnumerable data to insert
var dataToInsert = new List<YourEntity>
{
  // Your data to insert
};

var affectedRows = connection.BulkInsert(dataToInsert);
```

2. BulkUpdate
```cs
//Arrange IEnumerable data to update
var dataToUpdate = List<YourEntity>
{
  // Your changes
};

connection.BulkUpdate(dataToUpdate);
```

2. BulkDelete
```cs
//Arrange IEnumerable data to delete
var dataToDelete= List<YourEntity>
{
  // Rows to delete
};

connection.BulkDelete(dataToDelete);
```

### Queries
LaRoy ORM supports both synchronous and asynchronous queries.

1. Queries with dynamic result type.
```cs
IEnumerable<dynamic> dynamicResult = connection.Query("SELECT * FROM SampleTable st WHERE st.Id = @Param", new {Param = yourParameter});
// Or
IEnumerable<dynamic> dynamicAsyncResult = await connection.QueryAsync("SELECT * FROM SampleTable st WHERE st.Id = @Param", new {Param = yourParameter});
```

2. Queries with strict type results.
```cs
IEnumerable<YourType> strictResult = connection.Query<YourType>("SELECT * FROM SampleTable st WHERE st.Id = @Param", new {Param = yourParameter});
// Or
IEnumerable<YourType> strictAsyncResult = await connection.QueryAsync<YourType>("SELECT * FROM SampleTable st WHERE st.Id = @Param", new {Param = yourParameter});
```
Also can use `QueryFirst`, `QueryFirstOrDefault`, `QuerySingle`, and `QuerySingleOrDefault` methods with dynamic and strict type results.

All extension methods also accept context instead of connection.

You do not need to open or close the connection, it is handled inside of methods. 
But you can open your connection before sending it if needed.

## Contributing
If you have suggestions for how LaRoy ORM could be improved, or want to report a bug, open an issue! I'd love all and any contributions. Send me an email(orkhan.naghisoy@gmail.com)

## Notes
*Features not thoroughly tested use with caution.
















