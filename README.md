# LaRoy ORM

> An open-source free ORM for .NET to bring the best features of the most used ORMs, into one place

## Install

1. .NET CLI

```
dotnet add package LaRoy.ORM --version 1.0.4-beta
```

2. NuGet Package Manager

```
NuGet\Install-Package LaRoy.ORM -Version 1.0.4-beta
```

3. Paket CLI

```
paket add LaRoy.ORM --version 1.0.4-beta
```

## How to use

### Create a database connection(For now only MsSql, PostgreSQL and MySql supported)

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
  \\Your changes
};

connection.BulkUpdate(dataToUpdate);
```

2. BulkUpdate
```cs
//Arrange IEnumerable data to delete
var dataToDelete= List<YourEntity>
{
  // Rows to delete
};

connection.BulkDelete(dataToDelete);
```






















