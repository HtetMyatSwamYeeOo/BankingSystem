using BankingSystemWebsite.Models.Error;
using BankingSystemWebsite.Models.Transaction;
using BankingSystem_BL.BankAccount;
using BankingSystem_Common.Model;
using BankingSystem_Common.Constants;
using BankingSystemWebsite.Models.Error;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankingSystem_BL.Transaction;

namespace BankingSystemWebsite.Controllers.Transaction
{
    public class NewTransactionController : Controller
    {
        // GET: NewTransaction
        string usertype;

        public ActionResult newDepositTransaction()
        {

            string name = Convert.ToString(Session["UserName"]);

            if (name == null || name == "" || name == " ")
            {
                return RedirectToAction("login", "Login");
            }
            usertype = Convert.ToString(Session["LoginUserType"]).Trim();
            if (usertype == "Admin" || usertype == "staff")
            {
                return View();
            }
            else
            {
                ErrorObject eo = new ErrorObject();
                eo.usertype = this.usertype;
                eo.Error_Title = "Access Denied Warning";
                eo.Error_Controller_name = "Transaction";
                eo.Error_ActionResult_Method_name = "Transaction";
                eo.Error_Detail = "Access Denied Warning";
                eo.Error_Message = "You cannot Insert new Transaction because you do not have permission for that.";
                eo.Fix_Error = "To fix this Warning  you need to change your admin account.";
                Session["ErrorData"] = eo;
                return RedirectToAction("DeleteDenied", "ErrorPage", new { ErrorObject = eo });
            }

        }

        public ActionResult newWidthdrawTransaction()
        {

            string name = Convert.ToString(Session["UserName"]);

            if (name == null || name == "" || name == " ")
            {
                return RedirectToAction("login", "Login");
            }
            usertype = Convert.ToString(Session["LoginUserType"]).Trim();
            if (usertype == "Admin" || usertype == "staff")
            {
                return View();
            }
            else
            {
                ErrorObject eo = new ErrorObject();
                eo.usertype = this.usertype;
                eo.Error_Title = "Access Denied Warning";
                eo.Error_Controller_name = "Transaction";
                eo.Error_ActionResult_Method_name = "Transaction";
                eo.Error_Detail = "Access Denied Warning";
                eo.Error_Message = "You cannot Insert new Transaction because you do not have permission for that.";
                eo.Fix_Error = "To fix this Warning  you need to change your admin account.";
                Session["ErrorData"] = eo;
                return RedirectToAction("DeleteDenied", "ErrorPage", new { ErrorObject = eo });
            }

        }

        public ActionResult newInternalTransaction()
        {

            string name = Convert.ToString(Session["UserName"]);

            if (name == null || name == "" || name == " ")
            {
                return RedirectToAction("login", "Login");
            }
            usertype = Convert.ToString(Session["LoginUserType"]).Trim();
            if (usertype == "Admin" || usertype == "staff")
            {
                return View();
            }
            else
            {
                ErrorObject eo = new ErrorObject();
                eo.usertype = this.usertype;
                eo.Error_Title = "Access Denied Warning";
                eo.Error_Controller_name = "Transaction";
                eo.Error_ActionResult_Method_name = "Transaction";
                eo.Error_Detail = "Access Denied Warning";
                eo.Error_Message = "You cannot Insert new Internal Transaction Transfer because you do not have permission for that.";
                eo.Fix_Error = "To fix this Warning  you need to change your admin account.";
                Session["ErrorData"] = eo;
                return RedirectToAction("DeleteDenied", "ErrorPage", new { ErrorObject = eo });
            }

        }


        // InsertNewWidthdrawTransaction
        public ActionResult InsertNewTransaction(NewTransactionObject m)
        {
            string name = Convert.ToString(Session["UserName"]);

            if (name == null || name == "" || name == " ")
            {
                return RedirectToAction("login", "Login");
            }
            try
            {
                M_Transaction mc = new M_Transaction();
                mc.user_IBANo = (string.IsNullOrEmpty(m.user_IBANo)) ? string.Empty : m.user_IBANo.Trim();
                mc.From_IBANo = (string.IsNullOrEmpty(m.From_IBANo)) ? string.Empty : m.From_IBANo.Trim();
                mc.To_IBANo = (string.IsNullOrEmpty(m.To_IBANo)) ? string.Empty : m.To_IBANo.Trim();
                mc.acccode = (string.IsNullOrEmpty(m.acccode)) ? string.Empty : m.acccode.Trim();
                mc.trxtype = (string.IsNullOrEmpty(m.trxtype)) ? string.Empty : m.trxtype.Trim();
                //mc.trxdate = (string.IsNullOrEmpty(m.trxdate)) ? string.Empty : m.trxdate.Trim();
                mc.desc = m.desc;
                mc.amount = m.amount;
                mc.servicefeeamt = m.servicefeeamt;
                mc.totalamount = m.totalamount;

                mc.currcode = (string.IsNullOrEmpty(m.currcode)) ? string.Empty : m.currcode.Trim();
                
                if(m.currate == null)
                {
                    mc.currate = 1;
                }
                mc.remark = (string.IsNullOrEmpty(m.remark)) ? string.Empty : m.remark.Trim();
                mc.username = (string.IsNullOrEmpty(m.username)) ? string.Empty : m.username.Trim();
                mc.customeridno = (string.IsNullOrEmpty(m.customeridno)) ? string.Empty : m.customeridno.Trim();
                mc.bankname = (string.IsNullOrEmpty(m.bankname)) ? string.Empty : m.bankname.Trim();
                mc.createdby = Session["UserName"].ToString();



                if (TransactionBL.InsertNewTransaction(mc))
                {
                    //(new CreateUserActivityLog()).added(Session["UserName"].ToString(), " Customer " + mc.BankAccount_ID, Server.MapPath("~/Logs/ActivityLog"));
                    //BankAccountController.checkerDoAction = "New BankAccount is Added successfully!";
                    //BankAccountController.obj = BankAccountBL.SelectAllBankAccount();

                    return RedirectToAction("dashboard", "Dashboard");

                }//nn

                if(mc.trxtype=="1")
                {
                    return View("newDepositTransaction", "NewTransaction");
                }
                else if (mc.trxtype == "2")
                {
                    return View("newWidthdrawTransaction", "NewTransaction");
                }
                else
                {
                    return View("newInternalTransaction", "NewTransaction");
                }

            }
            catch (Exception ex)
            {
                //(new CreateLogFiles()).ErrorLogfile(ex, Server.MapPath("~/Logs/ErrorLog"));

                return View("newDepositTransaction", "NewTransaction");
            }




        }





    }
}