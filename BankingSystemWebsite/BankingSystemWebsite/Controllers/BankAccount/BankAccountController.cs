using BankingSystem_BL.BankAccount;
using BankingSystem_Common.Constants;
using BankingSystem_Common.Model;
using BankingSystemWebsite.Models.BankAccount;
using BankingSystemWebsite.Models.Error;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VEM_InventoryWebSite.Controllers.Shared;

namespace BankingSystemWebsite.Controllers.BankAccount
{
    public class BankAccountController : Controller
    {

        string usertype;
        public static bool NULLchecker;
        public static string checkerDoAction;
        public static string selectedItem;
        public static DataSet obj = null;
        public static DataSet Use_obj = null;
        public static BankingSystem_Common.Constants.IndexViewModel ViewModelobj;
        //===================================Action===========================================//

        // GET: BankAccount
        public ActionResult BankAccount(object sender, EventArgs e)
        {
            //check whether user is Login or not 
            string name = Convert.ToString(Session["UserName"]);
            if (name == null || name == "" || name == " ")
            {
                return RedirectToAction("login", "Login");
            }

            selectedItem = "All BankAccounts";
            //checked
            //Check if obj is null or not
            checkobj(obj);
            AddToObj(BankAccountBL.SelectAllBankAccount());

            ViewModelobj = getModel(1);
            return View(ViewModelobj);
        }


        //VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV for pagination VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV//
        [HttpGet]
        public ActionResult Index1(int? page = 1)
        {
            checkobj(obj);
            // setCount();
            getList();
            ViewModelobj = getModel(Convert.ToInt32(page));
            return View("BankAccount", ViewModelobj);
        }

        public BankingSystem_Common.Constants.IndexViewModel getModel(int page)
        {

            var pager = new Pager(0, page, 20);

            Use_obj = (DataSet)Session["Dataset1"];
            if (Use_obj != null)
            {
                pager = new Pager(Use_obj.Tables[0].Rows.Count, page, 20);

            }
            else
            {
                pager = new Pager(obj.Tables[0].Rows.Count, page, 20);
            }

            var viewModel = new BankingSystem_Common.Constants.IndexViewModel
            {

                Items = getDataset(pager.CurrentPage, pager.PageSize),
                Pager = pager,
                controller = "BankAccount"
            };

            TempData["Dataset"] = viewModel;

            return viewModel;
        }

        public DataSet getDataset(int curr, int size)
        {
            int start = (curr - 1) * size;

            DataSet newds = new DataSet();
            Use_obj = (DataSet)Session["Dataset1"];
            if (Use_obj.Tables[0].Rows.Count > 0 && Use_obj.Tables[0].Rows.Count > start)
            {
                IEnumerable<DataRow> mydatapage = Use_obj.Tables[0].AsEnumerable().Skip(start).Take(20);

                newds.Tables.Add(mydatapage.CopyToDataTable());
                NULLchecker = true;
            }
            else
            {
                newds.Tables.Add(new DataTable());
                NULLchecker = false;
            }
            return newds;
        }

        //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^for pagination^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^//


        //Get customers list  using 'All', 'Customers', 'Vendors', 'Inactive' 
        public ActionResult GetAllusingSelectedName(string name)
        {

            if (name == "Personal")
            {
                selectedItem = "Personal";
                checkobj(obj);
                AddToObj(BankAccountBL.SelectAllPersonalTypeBankAccount());
            }
            else if (name == "Business")
            {
                selectedItem = "Business";
                checkobj(obj);
                AddToObj(BankAccountBL.SelectAllBusinessTypeBankAccount());
            } 
            else
            {
                selectedItem = "All BankAccounts";
                checkobj(obj);
                AddToObj(BankAccountBL.SelectAllBankAccount());
            }



            return PartialView("_TableLayout", ViewModelobj);


        }

        private void AddToObj(object p)
        {
            throw new NotImplementedException();
        }

        //get sorted dataset 
        public ActionResult GetSorted(string sortname)
        {

            checkobj(obj);
            //copy obj without affecting obj
            DataSet tempds = obj.Copy();

            //To sort the table, we need to use DefaultView.
            DataView view = tempds.Tables[0].DefaultView;

            //Sort according to ***sortKey***
            if (sortname == "SortByName")
            {
                tempds.Tables[0].DefaultView.Sort = "Name ASC";
            }
            else if (sortname == "SortByCompanyName")
            {
                tempds.Tables[0].DefaultView.Sort = "Company Name ASC";
            }
            else if (sortname == "SortByCreatedTime")
            {
                tempds.Tables[0].DefaultView.Sort = "Created Time ASC";
            }
            else if (sortname == "SortByLastModifiedTime")
            {
                tempds.Tables[0].DefaultView.Sort = "Updated Time DESC";
            }

            //Create new dataset To retrieve the sorted Table.
            //DefaultView is Temporary. So we need to maintain it permanently in the new dataset.
            DataSet result = new DataSet();
            //Add Sorted Table to new Dataset's Table 
            result.Tables.Add(tempds.Tables[0].DefaultView.ToTable());

            AddToObj(result);

            return PartialView("_TableLayout", ViewModelobj);

        }

        //get selected name for dropdown 
        [HttpGet]
        public JsonResult GetSelectedName()
        {

            string temp = selectedItem;

            return Json(temp, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        public JsonResult GetCompanyName()
        {

            List<string> companyList = BankAccountBL.SelectAllCompanyName();
            List<string> lowerCaseCompanyList = new List<string>();
            for (int i = 0; i < companyList.Count; i++)
            {

                lowerCaseCompanyList.Add(companyList[i].ToLower());

            }
            ////List<BankAccountObject> temp1 =( List < BankAccountObject >) TempData["companyName"];

            return Json(lowerCaseCompanyList, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult BankAccount(NewBankAccountObject BankAccount)
        {
            checkobj(obj);
            NewBankAccountObject NewBankAccountObject = new NewBankAccountObject();
            NewBankAccountObject = BankAccount;
            AddToObj(obj);
            return View();
        }


        //Set all checked Item 'Active'
        public ActionResult GetAllCheckedItemActive()
        {
            try
            {
                checkobj(obj);
                //get All Checked Item from Table Partial View
                List<string> str = GetAllCheckedItems();

                //do nothing if there is no checked items
                if (str != null && str.Count > 0)
                {
                    foreach (string l in str)
                    {
                        M_BankAccount m = new M_BankAccount();
                        m = BankAccountBL.SelectAllBankAccountBYID(l);
                        if (m.BankAccount_IsActive == true)
                        {
                            checkerDoAction = "fc";
                        }
                        else if (BankAccountBL.UpdateBankAccountStatusByID(l, true, Session["UserName"].ToString()))
                        {
                            checkerDoAction = "s";
                           // (new CreateUserActivityLog()).MakeActiveInactive(Session["UserName"].ToString(), " Customer " + l, "Active", Server.MapPath("~/Logs/ActivityLog"));
                        }

                    }
                    emptyCheckedItem();
                    selectedItem = "All BankAccounts";
                    AddToObj(BankAccountBL.SelectAllBankAccount());
                }
                else
                    checkerDoAction = "f";

                return PartialView("_TableLayout", ViewModelobj);
            }
            catch (Exception ex)
            {
               // (new CreateLogFiles()).ErrorLogfile(ex, Server.MapPath("~/Logs/ErrorLog"));

                selectedItem = "All BankAccounts";
                AddToObj(BankAccountBL.SelectAllBankAccount());
                return PartialView("_TableLayout", ViewModelobj);
            }

        }

        //get list for search bar 
        public void getList()
        {
            List<BankAccountObject> BankAccountobjList = new List<BankAccountObject>();
            List<M_BankAccount> BankAccountList = BankAccountBL.SelectAllBankAccountName();
            for (int i = 0; i < BankAccountList.Count; i++)
            {
                BankAccountObject BankAccount = new BankAccountObject();

                BankAccount.BankAccountId = BankAccountList[i].BankAccount_ID;
                BankAccount.Name = BankAccountList[i].BankAccount_PersonName;
                BankAccountobjList.Add(BankAccount);
            }

            TempData["BankAccountName"] = BankAccountobjList;
        }

        //remove duplicate obj from list 
        public List<string> RemoveDuplicateObjectFromList(List<string> tempItems)
        {

            if (tempItems != null)
            {
                tempItems = tempItems.Distinct().ToList();
                return tempItems;
            }

            return null;

        }

        // get all BankAccount by name   for searchbox
        public ActionResult GetAllBankAccountByName(string name)
        {
            try
            {
                selectedItem = "All BankAccounts";
                if (name != "" || name != null)
                {
                    DataSet ds = new DataSet();
                    ds = BankAccountBL.SearchBankAccount(name);
                    if (ds != null)
                    {
                        AddToObj(ds);
                    }
                    else
                    {
                        selectedItem = "All BankAccounts";
                        AddToObj(BankAccountBL.SelectAllBankAccount());
                    }
                }
                else
                {
                    AddToObj(BankAccountBL.SelectAllBankAccount());
                }


                return PartialView("_TableLayout", ViewModelobj);
            }
            catch (Exception ex)
            {

               // (new CreateLogFiles()).ErrorLogfile(ex, Server.MapPath("~/Logs/ErrorLog"));

                selectedItem = "All BankAccounts";
                AddToObj(BankAccountBL.SelectAllBankAccount());
                return PartialView("_TableLayout", ViewModelobj);
            }
        }

        //search all item name 
        [HttpGet]
        public JsonResult SearchBankAccountInfo(string searchKey)
        {
            if (searchKey != "" || searchKey != null)
            {
                    List<BankAccountObject> BankAccountobjList = new List<BankAccountObject>();
                    List<M_BankAccount> BankAccountList = BankAccountBL.SelectAllBankAccountName();
                    for (int i = 0; i < BankAccountList.Count; i++)
                    {
                        BankAccountObject BankAccount = new BankAccountObject();

                        BankAccount.BankAccountId = BankAccountList[i].BankAccount_ID;
                        BankAccount.Name = BankAccountList[i].BankAccount_PersonName;
                        BankAccountobjList.Add(BankAccount);
                    }

                    TempData["BankAccountName"] = BankAccountobjList;
                      return Json(BankAccountobjList, JsonRequestBehavior.AllowGet);

            }
            else
            {
                List<BankAccountObject> BankAccountobjList = new List<BankAccountObject>();
                List<M_BankAccount> BankAccountList = BankAccountBL.SelectAllBankAccountName();
                for (int i = 0; i < BankAccountList.Count; i++)
                {
                    BankAccountObject BankAccount = new BankAccountObject();

                    BankAccount.BankAccountId = BankAccountList[i].BankAccount_ID;
                    BankAccount.Name = BankAccountList[i].BankAccount_PersonName;
                    BankAccountobjList.Add(BankAccount);
                }

                TempData["BankAccountName"] = BankAccountobjList;
                return Json(BankAccountobjList, JsonRequestBehavior.AllowGet);
            }
  
                //return View("newSale");
            
        }//end of SearchAllItemName method


        //Set all checked Item 'InActive'
        public ActionResult GetAllCheckedItemInactive()
        {
            string name = Convert.ToString(Session["UserName"]);
            if (name == null || name == "" || name == " ")
            {
                return RedirectToAction("login", "Login");
            }

            usertype = Convert.ToString(Session["LoginUserType"]).Trim();
            if (usertype != "Admin" || usertype != "Sale Admin")
            {
                //go to Access Denied Page 
                ErrorObject eo = new ErrorObject();
                eo.usertype = this.usertype;
                eo.Error_Title = "Access Denied Warning";
                eo.Error_Controller_name = "BankAccount";
                eo.Error_ActionResult_Method_name = "BankAccount";
                eo.Error_Detail = "BankAccount Inactive Access Denied Warning";
                eo.Error_Message = "You cannot INactive this  BankAccount because you are not admin user type";
                eo.Fix_Error = "To fix this Warning  you need to change your admin account";
                Session["ErrorData"] = eo;
                return RedirectToAction("DeleteDenied", "ErrorPage", new { ErrorObject = eo });


            }//end of if 
            else
            {
                try
                {
                    checkobj(obj);
                    //get All Checked Item from Table Partial View
                    List<string> str = GetAllCheckedItems();

                    //do nothing if there is no checked items
                    if (str != null && str.Count > 0)
                    {
                        foreach (string l in str)
                        {
                            M_BankAccount m = new M_BankAccount();
                            m = BankAccountBL.SelectAllBankAccountBYID(l);
                            if (m.BankAccount_IsActive == false)
                            {
                                checkerDoAction = "ai";
                            }
                            else if (BankAccountBL.UpdateBankAccountStatusByID(l, false, Session["UserName"].ToString()))
                            {
                                checkerDoAction = "s";
                               // (new CreateUserActivityLog()).MakeActiveInactive(Session["UserName"].ToString(), " Customer " + l, "Inactive", Server.MapPath("~/Logs/ActivityLog"));
                            }
                        }
                        emptyCheckedItem();
                        selectedItem = "All BankAccounts";
                        AddToObj(BankAccountBL.SelectAllBankAccount());
                    }
                    else checkerDoAction = "f";

                    return PartialView("_TableLayout", ViewModelobj);
                }
                catch (Exception ex)
                {

                   // (new CreateLogFiles()).ErrorLogfile(ex, Server.MapPath("~/Logs/ErrorLog"));


                    selectedItem = "All BankAccounts";
                    AddToObj(BankAccountBL.SelectAllBankAccount());
                    return PartialView("_TableLayout", ViewModelobj);
                }
            }//end of else


        }


        //delete BankAccount by id
        public ActionResult DelteBankAccountItemById()
        {
            //check user name 
            string name = Convert.ToString(Session["UserName"]);
            if (name == null || name == "" || name == " ")
            {
                return RedirectToAction("login", "Login");
            }

            usertype = Convert.ToString(Session["LoginUserType"]).Trim();
            if (usertype != "Admin" || usertype != "Sale Admin")
            {
                //go to Access Denied Page 
                ErrorObject eo = new ErrorObject();
                eo.usertype = this.usertype;
                eo.Error_Title = "Access Denied Warning";
                eo.Error_Controller_name = "BankAccount";
                eo.Error_ActionResult_Method_name = "BankAccount";
                eo.Error_Detail = "Delete Access Denied Warning";
                eo.Error_Message = "You cannot delete this  BankAccount because you are not admin user type";
                eo.Fix_Error = "To fix this Warning  you need to change your admin account";
                Session["ErrorData"] = eo;
                return RedirectToAction("DeleteDenied", "ErrorPage", new { ErrorObject = eo });


            }//end of if 
            else
            {

                try
                {
                    checkobj(obj);
                    //get All Checked Item from Table Partial View
                    List<string> str = GetAllCheckedItems();

                    //do nothing if there is no checked items
                    if (str != null && str.Count > 0)
                    {
                        foreach (string l in str)
                        {
                            if (BankAccountBL.DeleteBankAccountByID(l, Session["UserName"].ToString()))
                            {
                                checkerDoAction = "s";
                               // (new CreateUserActivityLog()).deleted(Session["UserName"].ToString(), " Customer " + l, Server.MapPath("~/Logs/ActivityLog"));
                            }
                        }
                        emptyCheckedItem();
                        selectedItem = "All BankAccounts";
                        AddToObj(BankAccountBL.SelectAllBankAccount());
                    }
                    else checkerDoAction = "f";
                    return PartialView("_TableLayout", ViewModelobj);
                }
                catch (Exception ex)
                {

                   // (new CreateLogFiles()).ErrorLogfile(ex, Server.MapPath("~/Logs/ErrorLog"));

                    return PartialView("_TableLayout", ViewModelobj);
                }//end of catch 


            }//end of (if)Check Admin or not 







        }//end of Delete Method 

        //======================Function=================================================//

        //For Retrieving the Checked Item from table Partial View
        public List<string> GetAllCheckedItems()
        {

            return _TableLayoutController.checkIdArr;
        }

        //For Adding new Dataset to Use_obj and obj
        //For Removing some columns for datashow
        //For setting TempData  
        public void AddToObj(DataSet ds)
        {
            getList();
            Use_obj = ds;

            //copy dataset without affecting ds
            obj = ds.Copy();

            //remove some column for show 
            //Use_obj.Tables[0].Columns.RemoveAt(0);
            //Use_obj.Tables[0].Columns.RemoveAt(5);
            //Use_obj.Tables[0].Columns.RemoveAt(5);
            //Use_obj.Tables[0].Columns.RemoveAt(5);
            Session["Dataset1"] = Use_obj;

            ViewModelobj = getModel(1);
            //store dataset tempolarily to use in next page
            TempData["Dataset"] = ViewModelobj;
        }

        //Empty the checked item list 
        public void emptyCheckedItem()
        {
            if (_TableLayoutController.checkIdArr != null)
            {
                _TableLayoutController.checkIdArr.Clear();
            }

        }

        //checking the checked item list is null
        [HttpGet]
        public JsonResult CheckNULL()
        {
            if (Use_obj == null)
            {
                NULLchecker = false;
            }

            if (Use_obj.Tables[0].Rows.Count <= 0)
            {
                NULLchecker = false;
            }

            string temp = NULLchecker != true ? "null" : "notNull";

            return Json(temp, JsonRequestBehavior.AllowGet);
        }

        //get current dropdown slected name 
        [HttpGet]
        public JsonResult GetCheckerName()
        {
            string temp = checkerDoAction;
            checkerDoAction = null;
            return Json(temp, JsonRequestBehavior.AllowGet);
        }

 

        //get all checked items
        public void checkobj(DataSet d)
        {
            if (d == null)
            {
                obj = BankAccountBL.SelectAllBankAccount();
            }
        }

        //=================================================================================//

    }
}