using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankingSystem_BL.User;
using BankingSystemWebsite.Models.User;

namespace BankingSystemWebsite.Controllers.UserAccount
{
    public class LoginController : Controller
    {
        UserObject user = new UserObject();
        public static string noti_string;
        // GET: Login
        public ActionResult login()
        {
            return View(user);
        }


        public ActionResult login_user(UserObject u)
        {

            try
            {
                if (u == null || u.password == null || u.username == null)
                {
                    noti_string = "PleaseEnter";
                    return View("login");
                }

                string encrypt_str = base64Encode(u.password);
                string pre_decrypt = u.password;
                u.password = encrypt_str;

                user = new UserObject();
                //  if (ModelState.IsValid)

                if (CheckedUser(u))
                {
                    Session["UserName123"] = u.username;
                    //FormsAuthentication.SetAuthCookie(user.username, user.rememberMe);


                    Session["UserName"] = UserBL.searchUserIDWithUserName(u.username);
                    Session["Password"] = u.password;
                    Session["UserType"] = UserBL.searchUserWithUserName(u.username);
                    Session["UserIDS"] = UserBL.searchUserIDWithUserName(u.username);
                    String LoginUserID = UserBL.searchUserIDWithUserName(u.username);

                    Session["LoginUserType"] = UserBL.searchUserTypeWithUserID(LoginUserID);
                    Session["UserType"] = UserBL.searchUserTypeWithUserID(LoginUserID);
                    String LoginUserSaleAssignIDforToday = UserBL.searchUserSaleAssignIDForToday(LoginUserID);
                    Session["LoginUserSaleAssignID"] = LoginUserSaleAssignIDforToday;

                    return RedirectToAction("dashboard", "Dashboard");
                }
                else
                {
                    noti_string = "Incorrect";
                    //ModelState.AddModelError("", "Log in Error ");
                    return View("login");
                }

            }
            catch (Exception ex)
            {
                noti_string = "Incorrect";
                return View("login");
            }
        }

        public ActionResult Logout()
        {
            Session["UserName"] = null;
            //FormsAuthentication.SignOut();
            return RedirectToAction("login", "Login");
        }

        public bool CheckedUser(UserObject user)
        {
            return UserBL.SearchNameAndPassword(user.username, user.password);
        }

        public void CheckLoginUserOrNo()
        {
            try
            {
                string name = Convert.ToString(Session["UserName"]);
                if (name == null || name == "" || name == " ")
                {
                    RedirectToAction("login", "Login");
                }
            }
            catch (Exception ex)
            {
            }
        }

        public string base64Encode(string sData) // Encode    
        {

            byte[] encData_byte = new byte[sData.Length];
            byte[] decode = new byte[sData.Length];

            encData_byte = System.Text.Encoding.UTF8.GetBytes(sData);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;


        }



    }
}