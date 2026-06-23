using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace CollaborativeCodingServer.Database
{
    public static class DatabaseTester
    {
        public static void Test()
        {
            try
            {
                using SqlConnection conn = DbConnectionFactory.GetConnection();

                conn.Open();

                Console.WriteLine("[DATABASE] Connected Successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[DATABASE ERROR]");

                Console.WriteLine(ex.Message);
            }
        }
    }
}
