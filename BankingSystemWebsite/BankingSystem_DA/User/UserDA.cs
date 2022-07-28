using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BankingSystem_Common.Constants;
using BankingSystem_Common.Model;
using BankingSystem_DA.Common;

namespace BankingSystem_DA.User
{
    public class UserDA
    {

        public static string SEARCH_USER_TYPE_WITH_USER_ID = "sps_search_user_type_with_user_id";
        public static string SEARCHNAMEANDPASSWORD = "sps_searchUser";
        public static string _INSERTUSER = "spi_insertuser";
        public static string SELECTALLUSERS = "sps_selectalluser";
        public static string _UPDATEUSER = "sps_update_user";
        public static string DELETEUSERBYID = "sps_deleteuserbyid";
        public static string SEARCHNAMEWITHUSERNAME = "sp_searchUserWithUserName";
        public static string SEARCH_USER_SALE_ASSIGN_ID_FOR_TODAY = "search_user_sale_assign_id_for_tody";
        public static string SEARCHUSERDETAILWITHUSERNAME = "sps_SearchUserDetailWithUserName";


        public static bool SearchNameAndPassword(string name, string password)
        {
            using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(SEARCHNAMEANDPASSWORD, con))
                {


                    try
                    {
                        DataSet dsContact = new DataSet("UserObj");



                        cmd.CommandType = CommandType.StoredProcedure;

                        //cmd.Parameters.AddWithValue("@name", name);
                        //cmd.Parameters.AddWithValue("@pwd", password);
                        cmd.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = name;
                        cmd.Parameters.Add("@pwd", SqlDbType.NVarChar, 100).Value = password;
                        con.Open();


                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;

                        da.Fill(dsContact);
                        int i = dsContact.Tables[0].Rows.Count;

                        M_User user = new M_User();

                        if (i != 0)
                        {
                            user.User_ID = Convert.ToString(dsContact.Tables[0].Rows[0]["User_ID"]);
                            user.User_AccName = Convert.ToString(dsContact.Tables[0].Rows[0]["User_AccName"]);
                            user.User_Password = Convert.ToString(dsContact.Tables[0].Rows[0]["User_Password"]);
                            user.User_Type = Convert.ToString(dsContact.Tables[0].Rows[0]["User_Type"]);



                            con.Close();
                            return true;


                        }


                        con.Close();
                        return false;
                    }
                    catch (Exception ex)
                    {
                        //(new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/ErrorLog"));
                        //(new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/Database_ErrorLog"));

                        return false;
                    }


                }
            }


        }

        public static string searchUserIDWithUserAccountName(string username)
        {
            using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(SEARCHNAMEWITHUSERNAME, con))
                {


                    try
                    {
                        DataSet dsContact = new DataSet("UserObj");



                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = username;
                        con.Open();


                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;

                        da.Fill(dsContact);
                        int i = dsContact.Tables[0].Rows.Count;

                        M_User user = new M_User();

                        if (i != 0)
                        {
                            user.User_ID = Convert.ToString(dsContact.Tables[0].Rows[0]["User_ID"]);
                            user.User_AccName = Convert.ToString(dsContact.Tables[0].Rows[0]["User_AccName"]);
                            user.User_Password = Convert.ToString(dsContact.Tables[0].Rows[0]["User_Password"]);
                            user.User_Type = Convert.ToString(dsContact.Tables[0].Rows[0]["User_Type"]);



                            con.Close();
                            return user.User_AccName;


                        }


                        con.Close();
                        return null;
                    }
                    catch (Exception ex)
                    {
                        //(new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/ErrorLog"));
                        //(new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/Database_ErrorLog"));

                        return null;
                    }


                }
            }
        }

        public static M_User searchUserDetailWithUserName(string l)
        {

            using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(SEARCHUSERDETAILWITHUSERNAME, con))
                {


                    try
                    {
                        DataSet dsContact = new DataSet("UserObj");



                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = l;
                        con.Open();


                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;

                        da.Fill(dsContact);
                        int i = dsContact.Tables[0].Rows.Count;

                        M_User user = new M_User();

                        if (i != 0)
                        {
                            user.User_ID = Convert.ToString(dsContact.Tables[0].Rows[0]["User_ID"]);
                            user.User_AccName = Convert.ToString(dsContact.Tables[0].Rows[0]["User_AccName"]);
                            user.User_Password = Convert.ToString(dsContact.Tables[0].Rows[0]["User_Password"]);
                            user.User_Type = Convert.ToString(dsContact.Tables[0].Rows[0]["User_Type"]);
                            user.Way_Name = Convert.ToString(dsContact.Tables[0].Rows[0]["Way_Description"]);



                            con.Close();
                            return user;


                        }


                        con.Close();
                        return null;
                    }
                    catch (Exception ex)
                    {
                        //(new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/ErrorLog"));
                        //(new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/Database_ErrorLog"));

                        return null;
                    }


                }
            }
        }


        public static string searchUserSaleAssignIDForToday(string LoginUserID)
        {
            using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(SEARCH_USER_SALE_ASSIGN_ID_FOR_TODAY, con))
                {


                    try
                    {
                        DataSet dsContact = new DataSet("UserObj");



                        cmd.CommandType = CommandType.StoredProcedure;

                        DateTime date = DateTime.Today;

                        cmd.Parameters.Add("@UID", SqlDbType.NVarChar, 20).Value = LoginUserID;
                        cmd.Parameters.Add("@date", SqlDbType.Date).Value = date;
                        con.Open();


                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;

                        da.Fill(dsContact);
                        String AssignSaleIDFortoday = Convert.ToString(dsContact.Tables[0].Rows[0]["Sale_Assign_ID"]);
                        //AS__06072018003256                                
                        con.Close();
                        return AssignSaleIDFortoday;
                    }
                    catch (Exception ex)
                    {
                        //(new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/ErrorLog"));
                        //(new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/Database_ErrorLog"));

                        return "Non";
                    }


                }
            }
        }

        public static object searchUserTypeWithUserID(string loginUserID)
        {
            using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(SEARCH_USER_TYPE_WITH_USER_ID, con))
                {

                    try
                    {
                        DataSet dsContact = new DataSet("UserObj");



                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.Parameters.Add("@UserID", SqlDbType.NVarChar, 100).Value = loginUserID;
                        con.Open();


                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;

                        da.Fill(dsContact);
                        int i = dsContact.Tables[0].Rows.Count;

                        M_User user = new M_User();

                        if (i != 0)
                        {
                            user.User_ID = Convert.ToString(dsContact.Tables[0].Rows[0]["User_ID"]);
                            user.User_AccName = Convert.ToString(dsContact.Tables[0].Rows[0]["User_AccName"]);
                            user.User_Password = Convert.ToString(dsContact.Tables[0].Rows[0]["User_Password"]);
                            user.User_Type = Convert.ToString(dsContact.Tables[0].Rows[0]["User_Type"]);



                            con.Close();
                            return user.User_Type;


                        }


                        con.Close();
                        return null;
                    }
                    catch (Exception ex)
                    {
                        //(new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/ErrorLog"));
                        //(new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/Database_ErrorLog"));

                        return null;
                    }


                }
            }
        }

        public static bool DeleteUserByID(string id, string deletedBy)
        {
            using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(DELETEUSERBYID, con))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("@Sale_ID", SaleID);
                        //cmd.Parameters.AddWithValue("@DeletedBy", DeletedBy);
                        cmd.Parameters.Add("@User_ID", SqlDbType.NChar, 20).Value = id;
                        cmd.Parameters.Add("@DeletedBy", SqlDbType.NChar, 10).Value = deletedBy;
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
                        //(new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/ErrorLog"));
                        //(new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/Database_ErrorLog"));

                        return false;
                    }

                }
            }

        }
        public static M_User SearchUser(string name, string password)
        {
            using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(SEARCHNAMEANDPASSWORD, con))
                {


                    try
                    {
                        DataSet dsContact = new DataSet("UserObj");



                        cmd.CommandType = CommandType.StoredProcedure;

                        //cmd.Parameters.AddWithValue("@name", name);
                        //cmd.Parameters.AddWithValue("@pwd", password);
                        cmd.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = name;
                        cmd.Parameters.Add("@pwd", SqlDbType.NVarChar, 100).Value = password;
                        con.Open();


                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;

                        da.Fill(dsContact);
                        int i = dsContact.Tables[0].Rows.Count;

                        M_User user = new M_User();

                        if (i != 0)
                        {
                            user.User_ID = Convert.ToString(dsContact.Tables[0].Rows[0]["User_ID"]);
                            user.User_AccName = Convert.ToString(dsContact.Tables[0].Rows[0]["User_AccName"]);
                            user.User_Password = Convert.ToString(dsContact.Tables[0].Rows[0]["User_Password"]);
                            user.User_Type = Convert.ToString(dsContact.Tables[0].Rows[0]["User_Type"]);



                            con.Close();
                            return user;


                        }


                        con.Close();
                        return null;
                    }
                    catch (Exception ex)
                    {
                        //(new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/ErrorLog"));
                        //(new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/Database_ErrorLog"));

                        return null;
                    }


                }
            }


        }


        public static string searchUserIDWithUserName(string name)
        {
            using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(SEARCHNAMEWITHUSERNAME, con))
                {


                    try
                    {
                        DataSet dsContact = new DataSet("UserObj");



                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = name;
                        con.Open();


                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;

                        da.Fill(dsContact);
                        int i = dsContact.Tables[0].Rows.Count;

                        M_User user = new M_User();

                        if (i != 0)
                        {
                            user.User_ID = Convert.ToString(dsContact.Tables[0].Rows[0]["User_ID"]);
                            user.User_AccName = Convert.ToString(dsContact.Tables[0].Rows[0]["User_AccName"]);
                            user.User_Password = Convert.ToString(dsContact.Tables[0].Rows[0]["User_Password"]);
                            user.User_Type = Convert.ToString(dsContact.Tables[0].Rows[0]["User_Type"]);



                            con.Close();
                            return user.User_ID;


                        }


                        con.Close();
                        return null;
                    }
                    catch (Exception ex)
                    {
                        //(new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/ErrorLog"));
                        //(new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/Database_ErrorLog"));

                        return null;
                    }


                }
            }


        }
        public static string searchUserWithUserName(string name)
        {
            using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(SEARCHNAMEWITHUSERNAME, con))
                {


                    try
                    {
                        DataSet dsContact = new DataSet("UserObj");



                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = name;
                        con.Open();


                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;

                        da.Fill(dsContact);
                        int i = dsContact.Tables[0].Rows.Count;

                        M_User user = new M_User();

                        if (i != 0)
                        {
                            user.User_ID = Convert.ToString(dsContact.Tables[0].Rows[0]["User_ID"]);
                            user.User_AccName = Convert.ToString(dsContact.Tables[0].Rows[0]["User_AccName"]);
                            user.User_Password = Convert.ToString(dsContact.Tables[0].Rows[0]["User_Password"]);
                            user.User_Type = Convert.ToString(dsContact.Tables[0].Rows[0]["User_Type"]);
                            //user.Way_Name = Convert.ToString(dsContact.Tables[0].Rows[0]["Way_Description"]);


                            con.Close();
                            return user.User_Type;


                        }


                        con.Close();
                        return null;
                    }
                    catch (Exception ex)
                    {
                        //(new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/ErrorLog"));
                        //(new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/Database_ErrorLog"));

                        return null;
                    }


                }
            }


        }
        public static bool UpdateUser(M_User user)
        {
            using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(_UPDATEUSER, con))
                {


                    try
                    {




                        cmd.CommandType = CommandType.StoredProcedure;

                        /* cmd.Parameters.AddWithValue("@name", user.User_AccName);
                         cmd.Parameters.AddWithValue("@password", user.User_Password);
                         cmd.Parameters.AddWithValue("@Updatedby", user.UpdatedBy);*/
                        cmd.Parameters.Add("@name", SqlDbType.NChar, 100).Value = user.User_AccName;
                        // cmd.Parameters.Add("@way", SqlDbType.NChar, 100).Value = user.User_WayName;
                        cmd.Parameters.Add("@password", SqlDbType.NVarChar, 100).Value = user.User_Password;
                        cmd.Parameters.Add("@type", SqlDbType.NVarChar, 100).Value = user.User_Type;
                        cmd.Parameters.Add("@Updatedby", SqlDbType.NVarChar, 100).Value = user.UpdatedBy;
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
                        //(new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/ErrorLog"));
                        //(new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/Database_ErrorLog"));

                        return false;
                    }


                }
            }


        }


        public static bool InsertUser(M_User user)
        {
            using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(_INSERTUSER, con))
                {


                    try
                    {




                        cmd.CommandType = CommandType.StoredProcedure;

                        //cmd.Parameters.AddWithValue("@name", user.User_AccName); 
                        //cmd.Parameters.AddWithValue("@password", user.User_Password);

                        cmd.Parameters.Add("@id", SqlDbType.NChar, 100).Value = user.User_ID;
                        cmd.Parameters.Add("@name", SqlDbType.NChar, 100).Value = user.User_AccName;
                        //cmd.Parameters.Add("@wayname", SqlDbType.NChar, 100).Value = user.User_WayName;

                        cmd.Parameters.Add("@password", SqlDbType.NVarChar, 100).Value = user.User_Password;
                        cmd.Parameters.Add("@type", SqlDbType.NVarChar, 100).Value = user.User_Type;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 100).Value = user.CreatedBy;
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
                        //(new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/ErrorLog"));
                        //(new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/Database_ErrorLog"));

                        return false;
                    }


                }
            }


        }
        public static DataSet SelectAllUsers()
        {
            using (SqlConnection con = new SqlConnection(CommonDA.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(SELECTALLUSERS, con))
                {


                    try
                    {




                        DataSet dsContact = new DataSet("User_Object");

                        //DataSet dsContact;



                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        con.Open();
                        da.Fill(dsContact);
                        con.Close();
                        return dsContact;

                    }
                    catch (Exception ex)
                    {
                        //(new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/ErrorLog"));
                        //(new CreateDALogFile()).ErrorLogfile(ex, HttpContext.Current.Server.MapPath("~/Logs/Database_ErrorLog"));

                        return null;
                    }


                }
            }


        }



    }

}
