﻿using BankingSystemWebsite.Models.BankAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankingSystemWebsite.Controllers.Shared
{
    public class _newBankAccountLayoutController : Controller
    {
        // GET: _newBankAccountLayout
            public ActionResult _newBankAccountLayout(string[] temp)
            {
                string name = Convert.ToString(Session["UserName"]);

                if (name == null || name == "" || name == " ")
                {
                    return RedirectToAction("login", "Login");
                }
                NewBankAccountObject newBankAccountObject = new NewBankAccountObject();



                return View();
            }



            [ChildActionOnly]
            public PartialViewResult DisplayPartialResultInsertForm()
            {
                NewBankAccountObject newBankAccountObject = new NewBankAccountObject();

                ///SetDataSet(BankAccountBL.SelectAllBankAccount());

                if (TempData["temp_array"] != null)
                {
                    string[] temp = (string[])TempData["temp_array"];
                    TempData["BankAccountPage"] = temp[0];
                    TempData["BankAccountController"] = temp[1];
                    TempData["BankAccountPageCancel"] = "BankAccount";
                    TempData["BankAccountControllerCancel"] = "BankAccount";

                }



                return PartialView("_newBankAccountLayout", new NewBankAccountObject());
            }


            [ChildActionOnly]
            public PartialViewResult editNewBankAccount()
            {

                if (TempData["temp_array"] != null)
                {
                    string[] temp = (string[])TempData["temp_array"];
                    TempData["BankAccountPage"] = temp[0];
                    TempData["BankAccountController"] = temp[1];
                    TempData["BankAccountPageCancel"] = "BankAccount";
                    TempData["BankAccountControllerCancel"] = "BankAccount";
                }

                NewBankAccountObject nco = new NewBankAccountObject();

                if (TempData["EditID"] != null)
                {
                    nco = (NewBankAccountObject)TempData["EditID"];
                }
                return PartialView("_newBankAccountLayout", nco);
            }





        

    }
}