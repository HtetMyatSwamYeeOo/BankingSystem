using BankingSystem_Common.Model;
using BankingSystem_DA.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem_DA.Transaction
{
    public class TransactionDA
    {
        public static bool InsertNewTransaction(M_Transaction mc)
        {
            using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("spi_newtransaction", con))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;    
                        cmd.Parameters.AddWithValue("@user_IBANo"  ,mc.user_IBANo.ToString());   
                        cmd.Parameters.AddWithValue("@From_IBANo    "  ,mc.From_IBANo.ToString());   
                        cmd.Parameters.AddWithValue("@To_IBANo" ,mc.To_IBANo.ToString());
                        cmd.Parameters.AddWithValue("@acccode"  ,mc.acccode.ToString()); 
                        cmd.Parameters.AddWithValue("@trxtype"  ,mc.trxtype.ToString()); 
                        cmd.Parameters.AddWithValue("@desc"  ,mc.desc.ToString());    
                        cmd.Parameters.AddWithValue("@amount" ,mc.amount.ToString());  
                        cmd.Parameters.AddWithValue("@servicefeeamt", mc.servicefeeamt.ToString());
                        cmd.Parameters.AddWithValue("@totalamount"  ,mc.totalamount.ToString());  
                        cmd.Parameters.AddWithValue("@currcode" ,mc.currcode.ToString());
                        cmd.Parameters.AddWithValue("@currate" ,mc.currate.ToString()); 
                        cmd.Parameters.AddWithValue("@remark" ,mc.remark.ToString());  
                        cmd.Parameters.AddWithValue("@username" ,mc.username.ToString());
                        cmd.Parameters.AddWithValue("@customeridno"  ,mc.customeridno.ToString()); 
                        cmd.Parameters.AddWithValue("@bankname"  ,mc.bankname.ToString());
                        cmd.Parameters.AddWithValue("@createdby", mc.createdby.ToString());

                        con.Open();

                        int i = cmd.ExecuteNonQuery();

                        if (i != 0)
                        {
                            con.Close();
                            return true;
                        }
                        else
                        {
                            con.Close();
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {

                        return false;
                    }

                }
            }
        }

        public static DataSet SelectAllBankAccount(string userid, string usertype)
        {
            using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("sps_selectalltransaction", con))
                {

                    try
                    {
                        DataSet dsBankAccount = new DataSet("Transaction_Object");

                        //DataSet dsBankAccount;

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@userid", userid);
                        cmd.Parameters.AddWithValue("@usertype", usertype);

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        con.Open();
                        da.Fill(dsBankAccount);
                        con.Close();
                        return dsBankAccount;
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
