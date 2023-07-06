using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaRoy.Mapper.BulkOperations.Utils
{
    public abstract class LaRoyDbContext
    {
        private readonly SqlConnection _connection;
        public LaRoyDbContext(string connectionString)
        {
                _connection = new SqlConnection(connectionString);
        }

        public SqlConnection GetConnection()
        {
            return _connection;
        }
    }
}
