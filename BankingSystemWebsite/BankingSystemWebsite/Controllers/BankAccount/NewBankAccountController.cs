using BankingSystem_BL.BankAccount;
using BankingSystem_Common;
using BankingSystem_Common.Constants;
using BankingSystem_Common.Model;
using BankingSystemWebsite.Models.BankAccount;
using BankingSystemWebsite.Models.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankingSystemWebsite.Controllers.BankAccount
{
    public class NewBankAccountController : Controller
    {
        // GET: NewBankAccount
        string usertype;
        // GET: newBankAccount
        public ActionResult newBankAccount()
        {

            string name = Convert.ToString(Session["UserName"]);

            if (name == null || name == "" || name == " ")
            {
                return RedirectToAction("login", "Login");
            }
            usertype = Convert.ToString(Session["LoginUserType"]).Trim();
            if (usertype == "Admin" || usertype == "Sale Admin" || usertype == "Sale Man")
            {
                return View();
            }
            else
            {
                ErrorObject eo = new ErrorObject();
                eo.usertype = this.usertype;
                eo.Error_Title = "Access Denied Warning";
                eo.Error_Controller_name = "BankAccount";
                eo.Error_ActionResult_Method_name = "BankAccount";
                eo.Error_Detail = "Access Denied Warning";
                eo.Error_Message = "You cannot Insert new BankAccount because you do not have permission for that.";
                eo.Fix_Error = "To fix this Warning  you need to change your admin account.";
                Session["ErrorData"] = eo;
                return RedirectToAction("DeleteDenied", "ErrorPage", new { ErrorObject = eo });
            }

        }

        [HttpGet]
        public JsonResult SearchAllWayName(string searchKey)
        {


            if (searchKey == "")
            {
                return null;
            }
            IList<NewBankAccountObject> BankAccountobject = new List<NewBankAccountObject>();
            List<M_BankAccount> waylist = BankAccountBL.SearchAllWayName(searchKey);

            for (int i = 0; i < waylist.Count; i++)
            {
                NewBankAccountObject NBankAccountObject = new NewBankAccountObject();
                NBankAccountObject.Way_ID = waylist[i].Way_ID;
                NBankAccountObject.Way_Name = waylist[i].Way_Name;

                BankAccountobject.Add(NBankAccountObject);
            }//end of for(int i=0;i<itemList.Count;i++)

            TempData["WayName"] = BankAccountobject;


            return Json(BankAccountobject, JsonRequestBehavior.AllowGet);
            //return View("newSale");


        }//end of SearchAllItemName method

        //insert new BankAccount
        public ActionResult InsertNewBankAccount(NewBankAccountObject m)
        {
            string name = Convert.ToString(Session["UserName"]);

            if (name == null || name == "" || name == " ")
            {
                return RedirectToAction("login", "Login");
            }
            try
            {
                M_BankAccount mc = new M_BankAccount();
                mc.BankAccount_ID = CommonFunction.createTimeStamp(Commons.BankAccountPrefix);
                mc.BankAccount_PersonName = (string.IsNullOrEmpty(m.Name)) ? string.Empty : m.Name.Trim(); ;
                mc.Name = (string.IsNullOrEmpty(m.Name)) ? string.Empty : m.Name.Trim(); ;
                mc.DateOfBirth = m.DateOfBirth;
                mc.Gender = (string.IsNullOrEmpty(m.Gender)) ? string.Empty : m.Gender.Trim(); ;
                mc.Nadtionaly = (string.IsNullOrEmpty(m.Nadtionaly)) ? string.Empty : m.Nadtionaly.Trim(); ;
                mc.IDNumber = (string.IsNullOrEmpty(m.IDNumber)) ? string.Empty : m.IDNumber.Trim(); ;
                mc.MonthlyIncome = m.MonthlyIncome;
                mc.CountryName = (string.IsNullOrEmpty(m.BankAccountPhone)) ? string.Empty : m.BankAccountPhone.Trim(); ;
                mc.BankAccount_MobilePhone = (string.IsNullOrEmpty(m.BankAccountPhone)) ? string.Empty : m.BankAccountPhone.Trim(); ;
                mc.BankAccount_CompanyName = (string.IsNullOrEmpty(m.CompanyName)) ? string.Empty : m.CompanyName.Trim(); ;
                mc.BankAccount_City = (string.IsNullOrEmpty(m.City)) ? string.Empty : m.City.Trim();
                mc.BankAccount_Email = (string.IsNullOrEmpty(m.Email)) ? string.Empty : m.Email.Trim();
                mc.BankAccount_IsActive = true;
                mc.BankAccount_Type = (string.IsNullOrEmpty(m.Type)) ? string.Empty : m.Type.Trim();
                mc.BankAccount_Address = (string.IsNullOrEmpty(m.Address)) ? string.Empty : m.Address.Trim();
                mc.CreatedBy = Session["UserName"].ToString();
                mc.Password = m.Password.ToString();

                if (BankAccountBL.InsertBankAccount(mc))
                {
                    //(new CreateUserActivityLog()).added(Session["UserName"].ToString(), " Customer " + mc.BankAccount_ID, Server.MapPath("~/Logs/ActivityLog"));
                    BankAccountController.checkerDoAction = "New BankAccount is Added successfully!";
                    BankAccountController.obj = BankAccountBL.SelectAllBankAccount();

                    return RedirectToAction("BankAccount", "BankAccount");

                }//nn

                return View("newBankAccount");

            }
            catch (Exception ex)
            {
                //(new CreateLogFiles()).ErrorLogfile(ex, Server.MapPath("~/Logs/ErrorLog"));

                return View("newBankAccount");
            }




        }






    }
}