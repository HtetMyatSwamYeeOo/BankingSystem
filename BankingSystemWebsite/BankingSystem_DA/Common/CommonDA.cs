using System;
using System.Data;
using System.Data.Odbc;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem_DA.Common
{
    class CommonDA
    {
        public static String getConnection()
        {
            OdbcConnection con = new OdbcConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["BankingSystem"].ConnectionString;
            return con.ConnectionString;
        }
    }
}
