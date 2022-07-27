using BankingSystem_DA.Dashboard;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem_DA.Common;

namespace BankingSystem_BL.Dashboard
{
    public class DashboardBL
    {

        //public static DataTable GetDashBoarddataDT()
        //{
        //    return DashboardDA.GetDashBoarddataDT();
        //}
        public static DataTable GetDashBoarddataDT(string name, string usertype)
        {
            return DashboardDA.GetDashBoarddataDT(name, usertype);
        }
    }
}
