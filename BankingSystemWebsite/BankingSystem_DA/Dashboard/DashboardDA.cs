using BankingSystem_DA.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem_DA.Dashboard
{
    public class DashboardDA
    {

        public static DataTable GetDashBoarddataDT(string name, string usertype)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("sps_selectalldashboarddata", con))
                {
                    try
                    {
                        SqlDataAdapter adp = new SqlDataAdapter(cmd);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@userid", name);
                        cmd.Parameters.AddWithValue("@usertype", usertype);

                        adp.Fill(dt);
          
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }


                }
            }
        }
    }
}
