using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankingSystem_BL.BankAccount;
using BankingSystem_Common.Model;
using BankingSystemWebsite.Models.BankAccount;
using BankingSystemWebsite.Models.Transaction;


namespace BankingSystemWebsite.Controllers.Shared
{
    public class _newTransactionLayoutController : Controller
    {
        // GET: _newTransactionLayout
        public ActionResult _newTransactionLayout(string[] temp)
        {
            string name = Convert.ToString(Session["UserName"]);

            if (name == null || name == "" || name == " ")
            {
                return RedirectToAction("login", "Login");
            }
            NewTransactionObject newTransactionObject = new NewTransactionObject();



            return View();
        }



        [ChildActionOnly]
        public PartialViewResult DisplayPartialResultInsertForm()
        {
            NewTransactionObject newTransactionObject = new NewTransactionObject();

            ///SetDataSet(TransactionBL.SelectAllTransaction());
            ///
            List<BankAccountObject> BankAccountobjList = new List<BankAccountObject>();
            List<M_BankAccount> BankAccountList = BankAccountBL.SelectAllBankAccountName();
            for (int i = 0; i < BankAccountList.Count; i++)
            {
                BankAccountObject BankAccount = new BankAccountObject();

                BankAccount.BankAccountId = BankAccountList[i].BankAccount_ID;
                BankAccount.Name = BankAccountList[i].BankAccount_PersonName;
                BankAccount.IDNumber = BankAccountList[i].IDNumber;
                BankAccountobjList.Add(BankAccount);
            }

            TempData["BankAccountName"] = BankAccountobjList;

            if (TempData["temp_array"] != null)
            {
                string[] temp = (string[])TempData["temp_array"];
                TempData["TransactionPage"] = temp[0];
                TempData["TransactionController"] = temp[1];
                TempData["TrxType"] = temp[2];
                TempData["TrxTypeDesc"] = temp[3];
                TempData["AccountCode"] = temp[4];
                TempData["TransactionPageCancel"] = "Transaction";
                TempData["TransactionControllerCancel"] = "Transaction";

            }



            return PartialView("_newTransactionLayout", new NewTransactionObject());
        }


        [ChildActionOnly]
        public PartialViewResult editNewTransaction()
        {

            if (TempData["temp_array"] != null)
            {
                string[] temp = (string[])TempData["temp_array"];
                TempData["TransactionPage"] = temp[0];
                TempData["TransactionController"] = temp[1];
                TempData["TransactionPageCancel"] = "Transaction";
                TempData["TransactionControllerCancel"] = "Transaction";
            }

            NewTransactionObject nco = new NewTransactionObject();

            if (TempData["EditID"] != null)
            {
                nco = (NewTransactionObject)TempData["EditID"];
            }
            return PartialView("_newTransactionLayout", nco);
        }



    }
}