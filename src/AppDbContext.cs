using LaRoy.Mapper.BulkOperations.Utils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaRoy.ORM
{
    public class AppDbContext : LaRoyDbContext
    {
        public override DbConnection CreateConnection()
            => new SqlConnection("");
    }
}
