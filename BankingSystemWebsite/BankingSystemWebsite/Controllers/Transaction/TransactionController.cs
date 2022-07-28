using BankingSystem_BL.Dashboard;
using BankingSystem_BL.Transaction;
using BankingSystem_Common.Constants;
using BankingSystem_Common.Model;
using BankingSystemWebsite.Models.Error;
using BankingSystemWebsite.Models.Transaction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VEM_InventoryWebSite.Controllers.Shared;

namespace BankingSystemWebsite.Controllers.Transaction
{
    public class TransactionController : Controller
    {

            string usertype;
            public static bool NULLchecker;
            public static string checkerDoAction;
            public static string selectedItem;
            public static DataSet obj = null;
            public static DataSet Use_obj = null;
            public static BankingSystem_Common.Constants.IndexViewModel ViewModelobj;
            //===================================Action===========================================//

            // GET: Transaction
            public ActionResult Transaction(object sender, EventArgs e)
            {
                //check whether user is Login or not 
                string name = Convert.ToString(Session["UserName"]);
                if (name == null || name == "" || name == " ")
                {
                    return RedirectToAction("login", "Login");
                }

                selectedItem = "All Transactions";
                usertype = Convert.ToString(Session["LoginUserType"]).Trim();
                    //checked
                    //Check if obj is null or not
                   //checkobj(obj);
                AddToObj(TransactionBL.SelectAllTransaction(name, usertype));

            DataTable dt = new DataTable();

            dt = DashboardBL.GetDashBoarddataDT(name, usertype);


            Session["DashBoadData"] = dt;

            ViewModelobj = getModel(1);
                return View(ViewModelobj);
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
                    controller = "Transaction"
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

            private void AddToObj(object p)
            {
                throw new NotImplementedException();
            }

            //get sorted dataset 
            //public ActionResult GetSorted(string sortname)
            //{

            //    checkobj(obj);
            //    //copy obj without affecting obj
            //    DataSet tempds = obj.Copy();

            //    //To sort the table, we need to use DefaultView.
            //    DataView view = tempds.Tables[0].DefaultView;

            //    //Sort according to ***sortKey***
            //    if (sortname == "SortByName")
            //    {
            //        tempds.Tables[0].DefaultView.Sort = "Name ASC";
            //    }
            //    else if (sortname == "SortByCompanyName")
            //    {
            //        tempds.Tables[0].DefaultView.Sort = "Company Name ASC";
            //    }
            //    else if (sortname == "SortByCreatedTime")
            //    {
            //        tempds.Tables[0].DefaultView.Sort = "Created Time ASC";
            //    }
            //    else if (sortname == "SortByLastModifiedTime")
            //    {
            //        tempds.Tables[0].DefaultView.Sort = "Updated Time DESC";
            //    }

            //    //Create new dataset To retrieve the sorted Table.
            //    //DefaultView is Temporary. So we need to maintain it permanently in the new dataset.
            //    DataSet result = new DataSet();
            //    //Add Sorted Table to new Dataset's Table 
            //    result.Tables.Add(tempds.Tables[0].DefaultView.ToTable());

            //    AddToObj(result);

            //    return PartialView("_TableLayout", ViewModelobj);

            //}

            //get selected name for dropdown 
            [HttpGet]
            public JsonResult GetSelectedName()
            {

                string temp = selectedItem;

                return Json(temp, JsonRequestBehavior.AllowGet);

            }


    
            //get list for search bar 
            public void getList()
            {
                List<TransactionObject> TransactionobjList = new List<TransactionObject>();
                List<M_Transaction> TransactionList = TransactionBL.SelectAllTransactionName();
                for (int i = 0; i < TransactionList.Count; i++)
                {
                    TransactionObject Transaction = new TransactionObject();

                    Transaction.From_IBANo = TransactionList[i].From_IBANo;
                    Transaction.To_IBANo = TransactionList[i].To_IBANo;
                    TransactionobjList.Add(Transaction);
                }

                TempData["TransactionName"] = TransactionobjList;
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

            // get all Transaction by name   for searchbox
            //search all item name 
            [HttpGet]
            public JsonResult SearchTransactionInfo(string searchKey)
            {
                if (searchKey != "" || searchKey != null)
                {
                    List<TransactionObject> TransactionobjList = new List<TransactionObject>();
                    List<M_Transaction> TransactionList = TransactionBL.SelectAllTransactionName();
                    for (int i = 0; i < TransactionList.Count; i++)
                    {
                        TransactionObject Transaction = new TransactionObject();
                    Transaction.From_IBANo = TransactionList[i].From_IBANo;
                    Transaction.To_IBANo = TransactionList[i].To_IBANo;
                    TransactionobjList.Add(Transaction);
                }

                TempData["TransactionName"] = TransactionobjList;
                    return Json(TransactionobjList, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    List<TransactionObject> TransactionobjList = new List<TransactionObject>();
                    List<M_Transaction> TransactionList = TransactionBL.SelectAllTransactionName();
                    for (int i = 0; i < TransactionList.Count; i++)
                    {
                        TransactionObject Transaction = new TransactionObject();

                    Transaction.From_IBANo = TransactionList[i].From_IBANo;
                    Transaction.To_IBANo = TransactionList[i].To_IBANo;
                    TransactionobjList.Add(Transaction);
                }

                TempData["TransactionName"] = TransactionobjList;
                    return Json(TransactionobjList, JsonRequestBehavior.AllowGet);
                }

                //return View("newSale");

            }//end of SearchAllItemName method



            //For Adding new Dataset to Use_obj and obj
            //For Removing some columns for datashow
            //For setting TempData  
            public void AddToObj(DataSet ds)
            {
                //getList();
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



            ////get all checked items
            //public void checkobj(DataSet d)
            //{
            //    if (d == null)
            //    {
            //        obj = (DataSet)TransactionBL.SelectAllTransaction();
            //    }
            //}

            //=================================================================================//

        

    }
}