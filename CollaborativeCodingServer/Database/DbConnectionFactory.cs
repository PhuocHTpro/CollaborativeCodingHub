using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace CollaborativeCodingServer.Database
{
    public static class DbConnectionFactory
    {
        private static readonly string connectionString = @"Server=.\SQLEXPRESS; Database=CollaborativeCodingDB; Trusted_Connection=True; TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}