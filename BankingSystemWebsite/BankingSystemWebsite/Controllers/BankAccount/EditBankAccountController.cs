using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankingSystem_BL.BankAccount;
using BankingSystem_Common;
using BankingSystem_Common.Constants;
using BankingSystem_Common.Model;
using BankingSystemWebsite.Models;
using BankingSystemWebsite.Models.BankAccount;
using BankingSystemWebsite.Models.Error;

namespace BankingSystemWebsite.Controllers.BankAccount
{
    public class EditBankAccountController : Controller
    {
        

        public static string _id;
        public static NewBankAccountObject NewBankAccountObject = null;
        string usertype;

        // GET: EditBankAccount
        public ActionResult editBankAccount()
        {
            string name = Convert.ToString(Session["UserName"]);

            if (name == null || name == "" || name == " ")
            {
                return RedirectToAction("login", "Login");
            }

            TempData["EditID"] = NewBankAccountObject;

            return View();
        }

        //
        public ActionResult editBankAccountByID(string id)
        {
            string name = Convert.ToString(Session["UserName"]);

            if (name == null || name == "" || name == " ")
            {
                return RedirectToAction("login", "Login");
            }
            try
            {
                usertype = Convert.ToString(Session["LoginUserType"]).Trim();
                if (usertype == "Admin" || usertype == "Staff")
                {
                    _id = id;
                    Session["editBankAccount"] = id;
                    M_BankAccount mc = BankAccountBL.SelectAllBankAccountBYID(id);

                    NewBankAccountObject nco = new NewBankAccountObject();

                    nco.BankAccountId = mc.BankAccount_ID;
                    nco.Name = mc.BankAccount_PersonName;
                    nco.DateOfBirth = mc.DateOfBirth;
                    nco.Gender = mc.Gender;
                    nco.Nadtionaly = mc.Nadtionaly;
                    nco.IDNumber = mc.IDNumber;
                    nco.MonthlyIncome = mc.MonthlyIncome;
                    nco.CountryName = mc.CountryName;
                    nco.BankAccountPhone = mc.BankAccount_MobilePhone;
                    nco.CompanyName = mc.BankAccount_CompanyName;
                    nco.City = mc.BankAccount_City;
                    nco.Email = mc.BankAccount_Email;
                    //nco.BankAccount_IsActive = mc.BankAccount_IsActive;
                    nco.Type = mc.BankAccount_Type;
                    nco.Address = mc.BankAccount_Address;
                    nco.Password = mc.Password;

                    NewBankAccountObject = nco;

                    TempData["EditID"] = nco;

                    return View("editBankAccount");


                }//end of if
                else
                {
                    ErrorObject eo = new ErrorObject();
                    eo.usertype = this.usertype;
                    eo.Error_Title = "Access Denied Warning";
                    eo.Error_Controller_name = "EditBankAccount";
                    eo.Error_ActionResult_Method_name = "editBankAccountByID";
                    eo.Error_Detail = "Edit  Access Denied Warning";
                    eo.Error_Message = "You cannont Edit this  BankAccount because you are not admin and Staff user type";
                    eo.Fix_Error = "To fix this Warning  you need to change your admin account";
                    Session["ErrorData"] = eo;
                    return RedirectToAction("DeleteDenied", "ErrorPage", new { ErrorObject = eo });

                }//end of else


            }//end of try
            catch (Exception ex)
            {
                //(new CreateLogFiles()).ErrorLogfile(ex, Server.MapPath("~/Logs/ErrorLog"));
                return View(RedirectToAction("BankAccount","BankAccount"));
            }
           
        }


        // update BankAccount   using the data from edit BankAccount page
        public ActionResult updateBankAccount(NewBankAccountObject NewBankAccountObject)
        {
            string name = Convert.ToString(Session["UserName"]);

            if (name == null || name == "" || name == " ")
            {
                return RedirectToAction("login", "Login");
            }
            try
            {
                usertype = Convert.ToString(Session["LoginUserType"]).Trim();
                if (usertype == "Admin" || usertype == "Sale Admin")
                {
                    _id = (string)Session["editBankAccount"];

                    NewBankAccountObject m = new NewBankAccountObject();
                    m = NewBankAccountObject;

                    //M_BankAccount _mc = BankAccountBL.SelectAllBankAccountBYID(_id);

                    M_BankAccount mc = new M_BankAccount();

                    mc.BankAccount_ID = _id;
                    mc.BankAccount_PersonName = (string.IsNullOrEmpty(m.Name)) ? string.Empty : m.Name.Trim(); ;
                    mc.Name = (string.IsNullOrEmpty(m.Name)) ? string.Empty : m.Name.Trim(); ;
                    mc.DateOfBirth = m.DateOfBirth;
                    mc.Gender = (string.IsNullOrEmpty(m.Gender)) ? string.Empty : m.Gender.Trim(); ;
                    mc.Nadtionaly = (string.IsNullOrEmpty(m.Nadtionaly)) ? string.Empty : m.Nadtionaly.Trim(); ;
                    mc.IDNumber = (string.IsNullOrEmpty(m.IDNumber)) ? string.Empty : m.IDNumber.Trim(); ;
                    mc.MonthlyIncome = m.MonthlyIncome;
                    mc.CountryName = (string.IsNullOrEmpty(m.CountryName)) ? string.Empty : m.CountryName.Trim(); ;
                    mc.BankAccount_MobilePhone = (string.IsNullOrEmpty(m.BankAccountPhone)) ? string.Empty : m.BankAccountPhone.Trim(); ;
                    mc.BankAccount_CompanyName = (string.IsNullOrEmpty(m.CompanyName)) ? string.Empty : m.CompanyName.Trim(); ;
                    mc.BankAccount_City = (string.IsNullOrEmpty(m.City)) ? string.Empty : m.City.Trim();
                    mc.BankAccount_Email = (string.IsNullOrEmpty(m.Email)) ? string.Empty : m.Email.Trim();
                    mc.BankAccount_IsActive = true;
                    mc.BankAccount_Type = (string.IsNullOrEmpty(m.Type)) ? string.Empty : m.Type.Trim();
                    mc.BankAccount_Address = (string.IsNullOrEmpty(m.Address)) ? string.Empty : m.Address.Trim();
                    mc.UpdatedBy = Session["UserName"].ToString();
                    mc.Password = m.Password.ToString();


                    if (BankAccountBL.UpdateBankAccountById(mc))
                    {
                        //customerOverviewController.checker = "Edit Successful";
                        //TranHistoryController.checker = "Edit Successful";
                        BankAccountController.obj = BankAccountBL.SelectAllBankAccount();
                        //customerOverviewController._id = _id;
                        return RedirectToAction("BankAccount", "BankAccount");
                    }


                    return View("editBankAccount");

                }//end of if
                else
                {
                    ErrorObject eo = new ErrorObject();
                    eo.usertype = this.usertype;
                    eo.Error_Title = "Access Denied Warning";
                    eo.Error_Controller_name = "TranHistory";
                    eo.Error_ActionResult_Method_name = "tranHistory";
                    eo.Error_Detail = "Edit  Access Denied Warning";
                    eo.Error_Message = "You cannont Edit this  BankAccount because you are not admin user type";
                    eo.Fix_Error = "To fix this Warning  you need to change your admin account";
                    Session["ErrorData"] = eo;
                    return RedirectToAction("DeleteDenied", "ErrorPage", new { ErrorObject = eo });
                }//end of else  
                                  

            }//end if try
            catch(Exception ex)
            {
                //(new CreateLogFiles()).ErrorLogfile(ex, Server.MapPath("~/Logs/ErrorLog"));
                return View("editBankAccount");
            }
        }



    }
}