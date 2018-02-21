using Olympics.Domain.Contracts.Enumerators;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olympics.NetFramework
{
    public static class ConfigurationProvider
    {
        public static string GetConnectionString(ConnectionStrings connectionString)
        {
            return ConfigurationManager.ConnectionStrings[connectionString.ToString()].ConnectionString;
        }
    }
}
