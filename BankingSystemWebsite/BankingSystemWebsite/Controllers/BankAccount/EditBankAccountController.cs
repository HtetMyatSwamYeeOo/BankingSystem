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
                if (usertype == "Admin" || usertype == "Sale Admin")
                {

                    //id =(string) TempData["EditID"];
                    _id = id;
                    Session["editBankAccount"] = id;
                    M_BankAccount mc = BankAccountBL.SelectAllBankAccountBYID(id);

                    NewBankAccountObject nco = new NewBankAccountObject();

                    nco.CompanyName = mc.BankAccount_CompanyName;
                    nco.Name = mc.BankAccount_PersonName;
                    nco.City = mc.BankAccount_City;
                    nco.Type = mc.BankAccount_Type;
                    nco.Note = mc.BankAccount_Notes;
                    nco.BankAccountPhone = mc.BankAccount_MobilePhone;
                    nco.Address = mc.BankAccount_Address;
                    nco.Way_Name = mc.Way_Name;
                    nco.Email = mc.BankAccount_Email;
                    nco.Discount_Percent = mc.BankAccount_Discount_percent;

                    NewBankAccountObject = nco;

                    TempData["EditID"] = nco;

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

                    M_BankAccount _mc = BankAccountBL.SelectAllBankAccountBYID(_id);

                    M_BankAccount mc = new M_BankAccount();

                    mc.BankAccount_ID = _id;
                    mc.BankAccount_Notes = (string.IsNullOrEmpty(m.Note)) ? string.Empty : m.Note.Trim();
                    mc.BankAccount_PersonName = m.Name.Trim();
                    mc.BankAccount_MobilePhone = (string.IsNullOrEmpty(m.BankAccountPhone)) ? string.Empty : m.BankAccountPhone.Trim(); ;
                    mc.BankAccount_CompanyName = (string.IsNullOrEmpty(m.CompanyName)) ? string.Empty : m.CompanyName.Trim(); ;
                    mc.BankAccount_WorkPhone = (string.IsNullOrEmpty(m.Workphone)) ? string.Empty : m.Workphone.Trim(); ;
                    mc.BankAccount_City = (string.IsNullOrEmpty(m.City)) ? string.Empty : m.City.Trim(); ;
                    mc.BankAccount_IsActive = _mc.BankAccount_IsActive;
                    mc.BankAccount_Type = (string.IsNullOrEmpty(m.Type)) ? string.Empty : m.Type.Trim();
                    mc.BankAccount_Address = (string.IsNullOrEmpty(m.Address)) ? string.Empty : m.Address.Trim();
                    mc.Way_Name = (string.IsNullOrEmpty(m.Way_Name)) ? string.Empty : m.Way_Name.Trim();
                    mc.BankAccount_Email = (string.IsNullOrEmpty(m.Email)) ? string.Empty : m.Email.Trim();
                    mc.BankAccount_Discount_percent = (string.IsNullOrEmpty(m.Discount_Percent)) ? string.Empty : m.Discount_Percent.Trim();
                    mc.UpdatedBy = Session["UserName"].ToString();

                    String WayName = (string.IsNullOrEmpty(mc.Way_Name)) ? string.Empty : mc.Way_Name.Trim(); ;
                    if (WayName != null)
                    {

                        String WayID = BankAccountBL.checkWayNameisAlreadyExitorNot(WayName);
                        if (WayID == null || WayID == "")
                        {
                            WayID = "NewWay";
                        }
                        if (WayID == "NewWay")
                        {
                            String Created_By = Session["UserIDS"].ToString();
                            Boolean InsertNewWay = BankAccountBL.InsertNewWaytoWayTable(WayName, Created_By);
                            if (InsertNewWay == true)
                            {
                                WayID = BankAccountBL.checkWayNameisAlreadyExitorNot(WayName);
                                mc.Way_ID = WayID;

                                if (BankAccountBL.UpdateBankAccountById(mc))

                                {
                                    // (new CreateUserActivityLog()).updated(Session["UserName"].ToString(), " Customer " + mc.BankAccount_ID, Server.MapPath("~/Logs/ActivityLog"));
                                    //customerOverviewController.checker = "Edit Successful";
                                    //TranHistoryController.checker = "Edit Successful";
                                    BankAccountController.obj = BankAccountBL.SelectAllBankAccount();
                                    //customerOverviewController._id = _id;
                                    return RedirectToAction("customerOverview", "customerOverview", _id);
                                }
                            }
                        }
                        else
                        {
                            WayID = BankAccountBL.checkWayNameisAlreadyExitorNot(WayName);
                            mc.Way_ID = WayID;

                            if (BankAccountBL.UpdateBankAccountById(mc))

                            {
                                // (new CreateUserActivityLog()).updated(Session["UserName"].ToString(), " Customer " + mc.BankAccount_ID, Server.MapPath("~/Logs/ActivityLog"));
                                //customerOverviewController.checker = "Edit Successful";
                                //TranHistoryController.checker = "Edit Successful";
                                BankAccountController.obj = BankAccountBL.SelectAllBankAccount();
                                //customerOverviewController._id = _id;
                                return RedirectToAction("customerOverview", "customerOverview", _id);
                            }
                        }
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