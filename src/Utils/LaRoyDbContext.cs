using System.Data.Common;

namespace LaRoy.Mapper.BulkOperations.Utils
{
    public abstract class LaRoyDbContext
    {
        private readonly DbConnection _connection;
        public LaRoyDbContext()
        {
            _connection = CreateConnection();
        }

        public DbConnection GetConnection()
        {
            return _connection;
        }

        public abstract DbConnection CreateConnection();
    }
}
