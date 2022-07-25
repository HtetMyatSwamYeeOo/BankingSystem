using BankingSystem_Common.Model;
using BankingSystem_DA.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem_DA.BankAccount
{
    public class BankAccountDA
    {
            static string INSERTNEWBankAccount = "InsertNewBankAccount";
            static string UPDATEBankAccountBYID = "UpdateBankAccountByID";
            static string DELETEBankAccountBYID = "DeleteBankAccountByID";
            static string SELECTBankAccountBYID = "SelectBankAccountByID";
            static string SELECTALLBankAccount = "SelectAllBankAccount";
            static string SELECTALLBankAccountBYDEALER = "SelectAllBankAccountByDealer";
            static string SELECTALLBankAccountBYSUBDEALER = "SelectAllBankAccountBySubDealer";
            static string SELECTALLBankAccountBYOUTLET = "SelectAllBankAccountByOutlet";
            static string SELECTALLBankAccountBYOEM = "SelectAllBankAccountByOem";
            static string SELECTALLBankAccountBYMARKETING = "SelectAllBankAccountByMarketing";
            static string SELECTALLBankAccountBYBUSLINE = "SelectAllBankAccountByBusline";
            static string UPDATEBankAccountSTATUSBYID = "UpdateBankAccountStatusByID";
            static string SELECTCUSTOMERIDBYNAME = "SelectCustomerIDByName";
            static string SELECTINVOICEBYBankAccountID = "SelectCustomerTransitionHistory";
            static string SELECTBILLBYBankAccountID = "SelectBillTransitionHistory";

            static string INSERTNEWWAY = "Insert_New_Way";

            static string CHECKWAYNAMEISALREADYEXITORNOT = "Check_Way_Name_is_Already_exit_or_not";
            static string SEARCHBankAccountNAME = "SearchCustomer";
            static string SELECTALLBankAccountNAME = "SelectAllBankAccountName";
            static string SELECTALLCOMPANYNAME = "SelectAllCompanyName";
            static string SELECTALLBankAccountBYCUSTOMERTYPE = "SelectAllBankAccountByCustomerType";
            static string SEARCHALLWAYNAMEFROMWAY = "SelectAllWayNameFromWay";


            public static List<M_BankAccount> SearchAllWayName(string searchKey)
            {
                using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand(SEARCHALLWAYNAMEFROMWAY, con))
                    {


                        try
                        {
                            DataSet ds = new DataSet("New_BankAccount_Object");


                            List<M_BankAccount> BankAccountList = new List<M_BankAccount>();
                            cmd.CommandType = CommandType.StoredProcedure;
                            //cmd.Parameters.AddWithValue("@search", searchChar);
                            cmd.Parameters.Add("@search", SqlDbType.NVarChar, 100).Value = searchKey;
                            SqlDataAdapter da = new SqlDataAdapter();
                            da.SelectCommand = cmd;
                            con.Open();
                            da.Fill(ds);
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)

                            {
                                M_BankAccount mc = new M_BankAccount();
                                mc.Way_ID = Convert.ToString(ds.Tables[0].Rows[i]["Way_Id"]);
                                mc.Way_Name = Convert.ToString(ds.Tables[0].Rows[i]["Way_Description"]);


                                BankAccountList.Add(mc);
                            }
                            con.Close();
                            return BankAccountList;
                        }
                        catch (Exception ex)
                        {
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/ErrorLog"));
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/Database_ErrorLog"));

                            return null;
                        }


                    }
                }
            }//end of search way name

            public static bool InsertNewWaytoWayTable(string wayName, string Created_By)
            {

                using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand(INSERTNEWWAY, con))
                    {
                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;


                            cmd.Parameters.Add("@Way_Name", SqlDbType.NVarChar, 100).Value = wayName;



                            //cmd.Parameters.Add("@CreatedBy", SqlDbType.NChar, 10).Value = Created_By;
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
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/ErrorLog"));
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/Database_ErrorLog"));

                            return false;
                        }

                    }
                }
            }

            public static string checkWayNameisAlreadyExitorNot(string wayName)
            {

                using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand(CHECKWAYNAMEISALREADYEXITORNOT, con))
                    {

                        try
                        {
                            //string path = HttpContext.Current.Server.MapPath("~/Logs/ErrorLog");
                            DataSet dsBankAccount = new DataSet("BankAccount_Object");

                            //DataSet dsBankAccount;



                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@Way_Name", SqlDbType.NVarChar, 20).Value = wayName.Trim();

                            SqlDataAdapter da = new SqlDataAdapter();
                            da.SelectCommand = cmd;
                            con.Open();
                            da.Fill(dsBankAccount);
                            con.Close();

                            String Way_ID = Convert.ToString(dsBankAccount.Tables[0].Rows[0]["Way_Id"]);

                            return Way_ID;
                        }
                        catch (Exception ex)
                        {
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/ErrorLog"));
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/Database_ErrorLog"));
                            return null;
                        }


                    }
                }

            }//end of CheckWaynameisAlreadExitorNot Function

            public static DataSet SelectAllBankAccount()
            {
                using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand("sps_SelectAllBankAccountName", con))
                    {

                        try
                        {
                            //string path = HttpContext.Current.Server.MapPath("~/Logs/ErrorLog");
                            DataSet dsBankAccount = new DataSet("BankAccount_Object");

                            //DataSet dsBankAccount;



                            cmd.CommandType = CommandType.StoredProcedure;

                            SqlDataAdapter da = new SqlDataAdapter();
                            da.SelectCommand = cmd;
                            con.Open();
                            da.Fill(dsBankAccount);
                            con.Close();
                            return dsBankAccount;
                        }
                        catch (Exception ex)
                        {
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/ErrorLog"));
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/Database_ErrorLog"));
                            return null;
                        }


                    }
                }
            }

            public static List<M_BankAccount> SelectAllBankAccountName()
            {
                using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand("sps_SelectAllBankAccountName", con))
                    {

                        try
                        {
                            DataSet dsBankAccount = new DataSet("BankAccount_Object");
                            List<M_BankAccount> BankAccountList = new List<M_BankAccount>();
                            cmd.CommandType = CommandType.StoredProcedure;

                            SqlDataAdapter da = new SqlDataAdapter();
                            da.SelectCommand = cmd;
                            con.Open();
                            da.Fill(dsBankAccount);
                            for (int i = 0; i < dsBankAccount.Tables[0].Rows.Count; i++)
                            {
                                M_BankAccount mc = new M_BankAccount();
                                mc.BankAccount_ID = Convert.ToString(dsBankAccount.Tables[0].Rows[i]["IBAN_ID"]);
                                mc.BankAccount_PersonName = Convert.ToString(dsBankAccount.Tables[0].Rows[i]["Name"]);
                                BankAccountList.Add(mc);
                            }
                            con.Close();
                            return BankAccountList;
                        }
                        catch (Exception ex)
                        {
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/ErrorLog"));
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/Database_ErrorLog"));

                            return null;
                        }


                    }
                }
            }


            public static List<string> SelectAllCompanyName()
            {
                using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand(SELECTALLCOMPANYNAME, con))
                    {

                        try
                        {
                            DataSet dsBankAccount = new DataSet("BankAccount_Object");
                            List<string> BankAccountList = new List<string>();
                            cmd.CommandType = CommandType.StoredProcedure;


                            SqlDataAdapter da = new SqlDataAdapter();
                            da.SelectCommand = cmd;
                            con.Open();
                            da.Fill(dsBankAccount);
                            for (int i = 0; i < dsBankAccount.Tables[0].Rows.Count; i++)
                            {
                                BankAccountList.Add(Convert.ToString(dsBankAccount.Tables[0].Rows[i]["BankAccount_CompanyName"]));
                            }
                            con.Close();
                            return BankAccountList;
                        }
                        catch (Exception ex)
                        {
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/ErrorLog"));
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/Database_ErrorLog"));

                            return null;
                        }


                    }
                }
            }
            public static DataSet SelectAllBankAccountByDealer()
            {
                using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand(SELECTALLBankAccountBYDEALER, con))
                    {

                        try
                        {
                            DataSet dsBankAccount = new DataSet("BankAccount_Object");



                            cmd.CommandType = CommandType.StoredProcedure;

                            SqlDataAdapter da = new SqlDataAdapter();
                            da.SelectCommand = cmd;
                            con.Open();
                            da.Fill(dsBankAccount);


                            con.Close();
                            return dsBankAccount;
                        }
                        catch (Exception ex)
                        {

                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/ErrorLog"));
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/Database_ErrorLog"));

                            return null;
                        }


                    }
                }
            }

            public static List<M_BankAccount> SelectAllBankAccountListByCustomerType(string type)
            {
                using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand(SELECTALLBankAccountBYCUSTOMERTYPE, con))
                    {

                        try
                        {
                            DataSet dsBankAccount = new DataSet("BankAccount_Object");

                            List<M_BankAccount> BankAccountList = new List<M_BankAccount>();

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@Customer_type", SqlDbType.NVarChar, 20).Value = type.Trim();

                            SqlDataAdapter da = new SqlDataAdapter();
                            da.SelectCommand = cmd;
                            con.Open();
                            da.Fill(dsBankAccount);

                            for (int i = 0; i < dsBankAccount.Tables[0].Rows.Count; i++)
                            {
                                M_BankAccount mc = new M_BankAccount();
                                mc.BankAccount_ID = Convert.ToString(dsBankAccount.Tables[0].Rows[i]["BankAccount_ID"]);
                                mc.BankAccount_PersonName = Convert.ToString(dsBankAccount.Tables[0].Rows[i]["Name"]);
                                mc.BankAccount_Type = Convert.ToString(dsBankAccount.Tables[0].Rows[i]["BankAccount Type"]);
                                mc.BankAccount_Discount_percent = Convert.ToString(dsBankAccount.Tables[0].Rows[i]["Discount Percent"]);
                                mc.BankAccount_CompanyName = Convert.ToString(dsBankAccount.Tables[0].Rows[i]["Company Name"]);


                                BankAccountList.Add(mc);
                            }

                            con.Close();
                            return BankAccountList;
                        }
                        catch (Exception ex)
                        {

                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/ErrorLog"));
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/Database_ErrorLog"));

                            return null;
                        }


                    }
                }
            }

            public static DataSet SelectAllBankAccountBySubDealer()
            {
                using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand(SELECTALLBankAccountBYSUBDEALER, con))
                    {

                        try
                        {
                            DataSet dsBankAccount = new DataSet("BankAccount_Object");



                            cmd.CommandType = CommandType.StoredProcedure;

                            SqlDataAdapter da = new SqlDataAdapter();
                            da.SelectCommand = cmd;
                            con.Open();
                            da.Fill(dsBankAccount);
                            con.Close();
                            return dsBankAccount;
                        }
                        catch (Exception ex)
                        {
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/ErrorLog"));
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/Database_ErrorLog"));

                            return null;
                        }


                    }
                }
            }


            public static DataSet SelectAllBankAccountByOutlet()
            {
                using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand(SELECTALLBankAccountBYOUTLET, con))
                    {

                        try
                        {
                            DataSet dsBankAccount = new DataSet("BankAccount_Object");



                            cmd.CommandType = CommandType.StoredProcedure;

                            SqlDataAdapter da = new SqlDataAdapter();
                            da.SelectCommand = cmd;
                            con.Open();
                            da.Fill(dsBankAccount);
                            con.Close();
                            return dsBankAccount;
                        }
                        catch (Exception ex)
                        {
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/ErrorLog"));
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/Database_ErrorLog"));

                            return null;
                        }


                    }
                }
            }

            public static DataSet SelectAllBankAccountByOem()
            {
                using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand(SELECTALLBankAccountBYOEM, con))
                    {

                        try
                        {
                            DataSet dsBankAccount = new DataSet("BankAccount_Object");



                            cmd.CommandType = CommandType.StoredProcedure;

                            SqlDataAdapter da = new SqlDataAdapter();
                            da.SelectCommand = cmd;
                            con.Open();
                            da.Fill(dsBankAccount);
                            con.Close();
                            return dsBankAccount;
                        }
                        catch (Exception ex)
                        {

                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/ErrorLog"));
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/Database_ErrorLog"));

                            return null;
                        }


                    }
                }
            }
            public static DataSet SelectAllBankAccountByMarketing()
            {
                using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand(SELECTALLBankAccountBYMARKETING, con))
                    {

                        try
                        {
                            DataSet dsBankAccount = new DataSet("BankAccount_Object");



                            cmd.CommandType = CommandType.StoredProcedure;

                            SqlDataAdapter da = new SqlDataAdapter();
                            da.SelectCommand = cmd;
                            con.Open();
                            da.Fill(dsBankAccount);
                            con.Close();
                            return dsBankAccount;
                        }
                        catch (Exception ex)
                        {

                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/ErrorLog"));
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/Database_ErrorLog"));

                            return null;
                        }


                    }
                }
            }
            public static DataSet SelectAllBankAccountByBusline()
            {
                using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand(SELECTALLBankAccountBYBUSLINE, con))
                    {

                        try
                        {
                            DataSet dsBankAccount = new DataSet("BankAccount_Object");



                            cmd.CommandType = CommandType.StoredProcedure;

                            SqlDataAdapter da = new SqlDataAdapter();
                            da.SelectCommand = cmd;
                            con.Open();
                            da.Fill(dsBankAccount);
                            con.Close();
                            return dsBankAccount;
                        }
                        catch (Exception ex)
                        {

                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/ErrorLog"));
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/Database_ErrorLog"));

                            return null;
                        }


                    }
                }
            }
            /*
             Author Name: Ye Min Aung
             Created Date: 17/5/2017
             Description: Insert BankAccount infos 
             Parameters: M_BankAccount
             Return: Boolean            
             */
            /*
             Updated By SoeMinHtet
             Created Date: 12/9/2017
             Description : AddWithValue to Add
             */
            public static Boolean InsertBankAccount(M_BankAccount BankAccount)
            {
                using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand("spi_insertnewbankaccount", con))
                    {
                        try
                        {

                        //cmd.Parameters.AddWithValue("@IBAN_ID", BankAccount.BankAccount_ID.ToString());
                        //cmd.Parameters.AddWithValue("@AccountType", BankAccount.BankAccount_Type.ToString());
                        //cmd.Parameters.AddWithValue("@Name", BankAccount.BankAccount_PersonName.ToString());
                        //cmd.Parameters.AddWithValue("@CompanyName", BankAccount.BankAccount_CompanyName.ToString());
                        //cmd.Parameters.AddWithValue("@DateOfBirth", BankAccount.DateOfBirth);
                        //cmd.Parameters.AddWithValue("@Phone", BankAccount.BankAccount_MobilePhone.ToString());
                        //cmd.Parameters.AddWithValue("@Gender", BankAccount.Gender.ToString());
                        //cmd.Parameters.AddWithValue("@Nationality", BankAccount.Nadtionaly.ToString());
                        //cmd.Parameters.AddWithValue("@Address", BankAccount.BankAccount_Address.ToString());
                        //cmd.Parameters.AddWithValue("@MonthlyIncome", BankAccount.MonthlyIncome.ToString());
                        //cmd.Parameters.AddWithValue("@BalanceAmount", BankAccount.BalanceAmount.ToString());
                        //cmd.Parameters.AddWithValue("@BankAccountName", BankAccount.Name.ToString());
                        //cmd.Parameters.AddWithValue("@email", BankAccount.BankAccount_Email.ToString());
                        //cmd.Parameters.AddWithValue("@IDNumber", BankAccount.IDNumber.ToString());
                        //cmd.Parameters.AddWithValue("@createby", BankAccount.CreatedBy.ToString());
                        //cmd.Parameters.AddWithValue("@password", BankAccount.Password.ToString());
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IBAN_ID", BankAccount.BankAccount_ID.ToString());
                        cmd.Parameters.AddWithValue("@AccountType", BankAccount.BankAccount_Type.ToString());
                        cmd.Parameters.AddWithValue("@Name", BankAccount.BankAccount_PersonName.ToString());
                        cmd.Parameters.AddWithValue("@CompanyName", BankAccount.BankAccount_CompanyName.ToString());
                        cmd.Parameters.AddWithValue("@DateOfBirth", BankAccount.DateOfBirth);
                        cmd.Parameters.AddWithValue("@Phone", BankAccount.BankAccount_MobilePhone.ToString());
                        cmd.Parameters.AddWithValue("@Gender", BankAccount.Gender.ToString());
                        cmd.Parameters.AddWithValue("@Nationality", BankAccount.Nadtionaly.ToString());
                        cmd.Parameters.AddWithValue("@Address", BankAccount.BankAccount_Address.ToString());
                        cmd.Parameters.AddWithValue("@MonthlyIncome", BankAccount.MonthlyIncome.ToString());
                        cmd.Parameters.AddWithValue("@BalanceAmount", BankAccount.BalanceAmount.ToString());
                        cmd.Parameters.AddWithValue("@BankAccountName", BankAccount.Name.ToString());
                        cmd.Parameters.AddWithValue("@email", BankAccount.BankAccount_Email.ToString());
                        cmd.Parameters.AddWithValue("@IDNumber", BankAccount.IDNumber.ToString());
                        cmd.Parameters.AddWithValue("@createby", BankAccount.CreatedBy.ToString());
                        cmd.Parameters.AddWithValue("@password", BankAccount.Password.ToString());

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


            /*
            Author Name: Ye Min Aung
            Created Date: 17/5/2017
            Description: Update BankAccount infos           
            Parameters: M_BankAccount
            Return: Boolean              
            */
            /*
              Updated by Khin La Pyae Oo
              Date : 12/9/2017
              Description : Change from AddWithValue to Add
             */
            public static Boolean UpdateBankAccountById(M_BankAccount BankAccount)
            {
                using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand(UPDATEBankAccountBYID, con))
                    {


                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            /*cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@BankAccount_ID", BankAccount.BankAccount_ID);
                            cmd.Parameters.AddWithValue("@BankAccount_PersonName", BankAccount.BankAccount_PersonName);
                            cmd.Parameters.AddWithValue("@BankAccount_Type", BankAccount.BankAccount_Type);
                            cmd.Parameters.AddWithValue("@BankAccount_CompanyName", BankAccount.BankAccount_CompanyName);
                            cmd.Parameters.AddWithValue("@BankAccount_MobilePhone", BankAccount.BankAccount_MobilePhone);
                            cmd.Parameters.AddWithValue("@BankAccount_WorkPhone", BankAccount.BankAccount_WorkPhone);
                            cmd.Parameters.AddWithValue("@BankAccount_Address", BankAccount.BankAccount_Address);
                            cmd.Parameters.AddWithValue("@BankAccount_City", BankAccount.BankAccount_City);
                            cmd.Parameters.AddWithValue("@BankAccount_Notes", BankAccount.BankAccount_Notes);
                            cmd.Parameters.AddWithValue("@BankAccount_IsActive", BankAccount.BankAccount_IsActive);
                            cmd.Parameters.AddWithValue("@UpdatedBy", BankAccount.UpdatedBy);*/

                            cmd.Parameters.Add("@BankAccount_ID", SqlDbType.NChar, 20).Value = BankAccount.BankAccount_ID;
                            cmd.Parameters.Add("@BankAccount_PersonName", SqlDbType.NVarChar, 100).Value = BankAccount.BankAccount_PersonName;
                            cmd.Parameters.Add("@BankAccount_Type", SqlDbType.NVarChar, 20).Value = BankAccount.BankAccount_Type;
                            cmd.Parameters.Add("@BankAccount_CompanyName", SqlDbType.NVarChar, 100).Value = BankAccount.BankAccount_CompanyName;
                            cmd.Parameters.Add("@BankAccount_MobilePhone", SqlDbType.NVarChar, 50).Value = BankAccount.BankAccount_MobilePhone;
                            cmd.Parameters.Add("@BankAccount_WorkPhone", SqlDbType.NVarChar, 50).Value = BankAccount.BankAccount_WorkPhone;
                            cmd.Parameters.Add("@BankAccount_Address", SqlDbType.NVarChar, 200).Value = BankAccount.BankAccount_Address;
                            cmd.Parameters.Add("@BankAccount_City", SqlDbType.NVarChar, 50).Value = BankAccount.BankAccount_City;
                            cmd.Parameters.Add("@BankAccount_Notes", SqlDbType.NVarChar, 200).Value = BankAccount.BankAccount_Notes;
                            cmd.Parameters.Add("@Way_ID", SqlDbType.NVarChar, 200).Value = BankAccount.Way_ID;
                            cmd.Parameters.Add("@BankAccount_Email", SqlDbType.NVarChar, 200).Value = BankAccount.BankAccount_Email;
                            cmd.Parameters.Add("@BankAccount_Discount_percent", SqlDbType.NVarChar, 200).Value = BankAccount.BankAccount_Discount_percent;
                            cmd.Parameters.Add("@BankAccount_IsActive", SqlDbType.Bit).Value = BankAccount.BankAccount_IsActive;
                            cmd.Parameters.Add("@UpdatedBy", SqlDbType.NChar, 10).Value = BankAccount.UpdatedBy;
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
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/ErrorLog"));
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/Database_ErrorLog"));

                            return false;
                        }



                    }
                }
            }

            /*
            Author Name: Ye Min Aung
            Created Date: 17/5/2017
            Description: Delete BankAccount infos    
            Parameters: BankAccountID,DeletedBy
            Return: Boolean                     
            */
            /*
                Updated By Khin La Pyae Oo
                Date : 12/9/2017
                Description : Change from AddWithValue to Add
            */
            public static Boolean DeleteBankAccountByID(string BankAccountID, string DeletedBy)
            {
                using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand(DELETEBankAccountBYID, con))
                    {

                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            /*cmd.Parameters.AddWithValue("@BankAccount_ID", BankAccountID);
                            cmd.Parameters.AddWithValue("@DeletedBy", DeletedBy);*/
                            cmd.Parameters.Add("@BankAccount_ID", SqlDbType.NChar, 20).Value = BankAccountID;
                            cmd.Parameters.Add("@DeletedBy", SqlDbType.NChar, 10).Value = DeletedBy;

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
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/ErrorLog"));
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/Database_ErrorLog"));

                            return false;
                        }


                    }
                }
            }

            //Select all BankAccount by Customer y Customer


            /*
            Author Name: Aung Myo Myint
            Created Date: 25/5/2017
            Description: Update BankAccount infos 
            Parameters: BankAccountiD, is_active
            Return: Boolean            
            */
            /* 
                Updated By Khin La Pyae Oo
                Date : 12/9/2017
                Description : Change from AddWithValue to Add
             */

            public static Boolean UpdateBankAccountStatusByID(string BankAccount_ID, bool BankAccount_IsActive, string updatedby)
            {
                using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand(UPDATEBankAccountSTATUSBYID, con))
                    {

                        try
                        {
                            /*cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@BankAccount_ID", BankAccount_ID);
                            cmd.Parameters.AddWithValue("@BankAccount_IsActive", BankAccount_IsActive);
                            cmd.Parameters.AddWithValue("@Updatedby", updatedby);*/
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@BankAccount_ID", SqlDbType.NChar, 20).Value = BankAccount_ID;
                            cmd.Parameters.Add("@BankAccount_IsActive", SqlDbType.Bit).Value = BankAccount_IsActive;
                            cmd.Parameters.Add("@UpdatedBy", SqlDbType.NChar, 50).Value = updatedby;
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
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/ErrorLog"));
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/Database_ErrorLog"));

                            return false;
                        }


                    }
                }
            }

            /*
    Author Name: Aung Myo Myint
    Created Date: 29/5/2017
    Description: Select BankAccount by ID 
    Parameters: BankAccountiD
    Return: Boolean            
    */
            /*
                Updated By Khin La Pyae Oo
                Date : 12/9/2017
                Description : Change from AddWithValue to Add
            */

            //
            public static M_BankAccount SelectAllBankAccountBYID(string BankAccount_ID)
            {
                using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand(SELECTBankAccountBYID, con))
                    {

                        try
                        {
                            DataSet dsBankAccount = new DataSet("BankAccount_Object");

                            M_BankAccount mBankAccount = new M_BankAccount();

                            cmd.CommandType = CommandType.StoredProcedure;
                            //cmd.Parameters.AddWithValue("@BankAccount_ID", BankAccount_ID);
                            cmd.Parameters.Add("@BankAccount_ID", SqlDbType.NChar, 20).Value = BankAccount_ID;
                            con.Open();

                            SqlDataAdapter da = new SqlDataAdapter();
                            da.SelectCommand = cmd;

                            da.Fill(dsBankAccount);


                            mBankAccount.BankAccount_ID = Convert.ToString(dsBankAccount.Tables[0].Rows[0]["BankAccount_ID"]);
                            mBankAccount.BankAccount_PersonName = Convert.ToString(dsBankAccount.Tables[0].Rows[0]["BankAccount_PersonName"]);
                            mBankAccount.BankAccount_Type = Convert.ToString(dsBankAccount.Tables[0].Rows[0]["BankAccount_Type"]);
                            mBankAccount.BankAccount_CompanyName = Convert.ToString(dsBankAccount.Tables[0].Rows[0]["BankAccount_CompanyName"]);
                            mBankAccount.BankAccount_MobilePhone = Convert.ToString(dsBankAccount.Tables[0].Rows[0]["BankAccount_MobilePhone"]);
                            mBankAccount.BankAccount_WorkPhone = Convert.ToString(dsBankAccount.Tables[0].Rows[0]["BankAccount_WorkPhone"]);
                            mBankAccount.BankAccount_Address = Convert.ToString(dsBankAccount.Tables[0].Rows[0]["BankAccount_Address"]);
                            mBankAccount.BankAccount_City = Convert.ToString(dsBankAccount.Tables[0].Rows[0]["BankAccount_City"]);
                            mBankAccount.BankAccount_Notes = Convert.ToString(dsBankAccount.Tables[0].Rows[0]["BankAccount_Notes"]);
                            mBankAccount.Way_Name = Convert.ToString(dsBankAccount.Tables[0].Rows[0]["Way_Description"]);
                            mBankAccount.BankAccount_Email = Convert.ToString(dsBankAccount.Tables[0].Rows[0]["BankAccount_Email"]);
                            mBankAccount.BankAccount_Discount_percent = Convert.ToString(dsBankAccount.Tables[0].Rows[0]["BankAccount_Discount_Percent"]);
                            mBankAccount.BankAccount_IsActive = Convert.ToBoolean(dsBankAccount.Tables[0].Rows[0]["BankAccount_isActive"]);
                            con.Close();
                            return mBankAccount;
                        }
                        catch (Exception ex)
                        {
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/ErrorLog"));
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/Database_ErrorLog"));

                            return null;
                        }


                    }
                }
            }
            /*
                Updated By Khin La Pyae Oo
                Date : 12/9/2017
                Description : Change from AddWithValue to Add
            */
            public static M_BankAccount SelectCustomerIDbyName(string name)
            {
                using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand(SELECTCUSTOMERIDBYNAME, con))
                    {
                        try
                        {
                            DataSet dsBankAccount = new DataSet("BankAccount_Object");



                            cmd.CommandType = CommandType.StoredProcedure;

                            //cmd.Parameters.AddWithValue("@BankAccount_ID", name);
                            cmd.Parameters.Add("@BankAccount_ID", SqlDbType.NChar, 10).Value = name;
                            con.Open();


                            SqlDataAdapter da = new SqlDataAdapter();
                            da.SelectCommand = cmd;

                            da.Fill(dsBankAccount);

                            int i = dsBankAccount.Tables[0].Rows.Count;

                            M_BankAccount mc = new M_BankAccount();

                            if (i != 0)
                            {
                                mc.BankAccount_ID = Convert.ToString(dsBankAccount.Tables[0].Rows[0]["BankAccount_ID"]);
                                mc.BankAccount_PersonName = Convert.ToString(dsBankAccount.Tables[0].Rows[0]["BankAccount_PersonName"]);
                                mc.CreatedBy = Convert.ToString(dsBankAccount.Tables[0].Rows[0]["CreatedBy"]);

                                con.Close();

                                return mc;
                            }


                            con.Close();
                            return null;
                        }
                        catch (Exception ex)
                        {
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/ErrorLog"));
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/Database_ErrorLog"));

                            return null;
                        }



                    }
                }
            }


            /*
                Updated By Khin La Pyae Oo
                Date : 12/9/2017
                Description : Change from AddWithValue to Add
            */

            public static DataSet SelectAllTransactionBYBankAccountID(string BankAccountID)
            {
                using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand(SELECTINVOICEBYBankAccountID, con))
                    {
                        try
                        {
                            DataSet dsBankAccount = new DataSet("BankAccount_Object");



                            cmd.CommandType = CommandType.StoredProcedure;

                            //cmd.Parameters.AddWithValue("@BankAccountID", BankAccountID);
                            cmd.Parameters.Add("@BankAccountID", SqlDbType.NChar, 20).Value = BankAccountID;
                            con.Open();


                            SqlDataAdapter da = new SqlDataAdapter();
                            da.SelectCommand = cmd;

                            da.Fill(dsBankAccount);

                            if (dsBankAccount == null)
                            {
                                con.Close();
                                return null;
                            }
                            else
                            {
                                con.Close();
                                return dsBankAccount;
                            }





                        }
                        catch (Exception ex)
                        {
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/ErrorLog"));
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/Database_ErrorLog"));

                            return null;
                        }
                    }
                }
            }



            /*
                Updated By Khin La Pyae Oo
                Date : 12/9/2017
                Description : Change from AddWithValue to Add
            */
            public static DataSet SelectAllTransactionBillBYBankAccountID(string BankAccountID)
            {
                using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand(SELECTBILLBYBankAccountID, con))
                    {
                        try
                        {
                            DataSet dsBankAccount = new DataSet("BankAccount_Object");



                            cmd.CommandType = CommandType.StoredProcedure;

                            //cmd.Parameters.AddWithValue("@BankAccountID", BankAccountID);
                            cmd.Parameters.Add("@BankAccountID", SqlDbType.NChar, 20).Value = BankAccountID;
                            con.Open();


                            SqlDataAdapter da = new SqlDataAdapter();
                            da.SelectCommand = cmd;

                            da.Fill(dsBankAccount);
                            if (dsBankAccount == null)
                            {
                                con.Close();
                                return null;
                            }
                            else
                            {
                                con.Close();
                                return dsBankAccount;
                            }

                        }
                        catch (Exception ex)
                        {
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/ErrorLog"));
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/Database_ErrorLog"));

                            return null;
                        }
                    }
                }
            }

            /*Search BankAccount 
             Author Name=Let Yee
             Decription=Search customer from search bar
            Date=14/7/2017
            */
            /*
                Updated By Khin La Pyae Oo
                Date : 12/9/2017
                Description : Change from AddWithValue to Add
            */
            public static DataSet SearchBankAccount(string name)
            {
                using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand(SEARCHBankAccountNAME, con))
                    {
                        try
                        {
                            DataSet dsBankAccount = new DataSet("BankAccount_Object");
                            cmd.CommandType = CommandType.StoredProcedure;
                            //cmd.Parameters.AddWithValue("@search", name);
                            cmd.Parameters.Add("@search", SqlDbType.NVarChar, 100).Value = name;
                            SqlDataAdapter da = new SqlDataAdapter();
                            da.SelectCommand = cmd;

                            con.Open();
                            da.Fill(dsBankAccount);
                            con.Close();
                            return dsBankAccount;
                        }
                        catch (Exception ex)
                        {
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/ErrorLog"));
                          //  (new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/Database_ErrorLog"));

                            return null;
                        }

                    }
                }
            }



    }
}
