using Olympics.Domain.Contracts.Enumerators;
using Olympics.NetFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olympics.DataAccess.Factories
{
    public static class ConnectionFactory
    {
        public static IDbConnection GetConnection(ConnectionStrings connStr)
        {
            IDbConnection con = new SqlConnection(ConfigurationProvider.GetConnectionString(connStr));

            con.Open();

            return con;
        }
    }
}
