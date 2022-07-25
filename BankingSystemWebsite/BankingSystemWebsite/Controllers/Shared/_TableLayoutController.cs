using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankingSystem_BL.BankAccount;
using BankingSystem_Common.Constants;
using BankingSystem_Common.Model;
using BankingSystemWebsite.Models;


namespace VEM_InventoryWebSite.Controllers.Shared
{
    public class _TableLayoutController : Controller
    {

        public DataSet Obj;
        public static List<string> checkIdArr;
        public static BankingSystem_Common.Constants.IndexViewModel ViewModelobj;

        public _TableLayoutController()
        {

            
        }

        public _TableLayoutController(DataSet dataSet)
        {

            SetDataSet(dataSet);
        }

        // GET: _TableLayout
        public ActionResult Index()
        {
            

            return View();
        }

        [HttpGet]
        public ActionResult Index(int? page=1)
        {
            Obj = (DataSet)Session["TableLayout"];
            var pager = new Pager(Obj.Tables[0].Rows.Count, page, 30);

            var viewModel = new BankingSystem_Common.Constants.IndexViewModel
            {
                
                Items = getDataset(pager.CurrentPage, pager.PageSize),
                Pager = pager
            };

            return PartialView("_TableLayout", viewModel);
        }

    
        //get dataset 
        public DataSet getDataset(int curr, int size)
        {
            int start = (curr - 1) * size;

            Obj = (DataSet )Session["TableLayout"];

            DataSet newds = new DataSet();
            

            IEnumerable<DataRow> mydatapage = Obj.Tables[0].AsEnumerable().Skip(start).Take(30);

            newds.Tables.Add(mydatapage.CopyToDataTable());

            return newds;
        }

           //display partial  result 
        [ChildActionOnly]
        public PartialViewResult DisplayPartialResult(object sender, EventArgs e)
        {
            //SetDataSet(ContactBL.SelectAllContact());
            try
            {
               

                if (TempData["Dataset"] != null)
                {
                    ViewModelobj = new BankingSystem_Common.Constants.IndexViewModel();
                    ViewModelobj = (BankingSystem_Common.Constants.IndexViewModel)TempData["Dataset"];


                    SetDataSet(ViewModelobj.Items);

                    return PartialView("_TableLayout", ViewModelobj);
                }
                
                return PartialView("_TableLayout", null);
            }
            catch (Exception ex)
            {
               // (new CreateLogFiles()).ErrorLogfile(ex, Server.MapPath("~/Logs/ErrorLog"));
                return PartialView("_TableLayout", null);
            }

        }

        //display parintal Result with button
        [ChildActionOnly]
        public PartialViewResult DisplayPartialResultWithButton(object sender, EventArgs e)
        {

            try
            {
                if (TempData["Dataset"]!=null)
                {
                    ViewModelobj = new BankingSystem_Common.Constants.IndexViewModel();
                    ViewModelobj =(BankingSystem_Common.Constants.IndexViewModel) TempData["Dataset"];


                    SetDataSet(ViewModelobj.Items);
                  
                    return PartialView("_TableLayoutWithButtonView", ViewModelobj);
                }
                return PartialView("_TableLayoutWithButtonView", null);
            }
            catch (Exception ex)
            {
               // (new CreateLogFiles()).ErrorLogfile(ex, Server.MapPath("~/Logs/ErrorLog"));
                return PartialView("_TableLayoutWithButtonView", null);
            }
            ///SetDataSet(ContactBL.SelectAllContact());
           
        }

        [ChildActionOnly]
        public PartialViewResult DisplayPartialResultWithDetailButton(object sender, EventArgs e)
        {

            try
            {
                if (TempData["Dataset"] != null)
                {
                    ViewModelobj = new BankingSystem_Common.Constants.IndexViewModel();
                    ViewModelobj = (BankingSystem_Common.Constants.IndexViewModel)TempData["Dataset"];


                    SetDataSet(ViewModelobj.Items);

                    return PartialView("_TableLayoutWithDetailButtonView", ViewModelobj);
                }
                return PartialView("_TableLayoutWithDetailButtonView", null);
            }
            catch (Exception ex)
            {
               // (new CreateLogFiles()).ErrorLogfile(ex, Server.MapPath("~/Logs/ErrorLog"));
                return PartialView("_TableLayoutWithDetailButtonView", null);
            }
            ///SetDataSet(ContactBL.SelectAllContact());

        }

        [ChildActionOnly]
        public PartialViewResult DisplayPartialResultWithButtonForReturn()
        {
            try
            {
                if (TempData["Dataset"] != null)
                {
                    ViewModelobj = new BankingSystem_Common.Constants.IndexViewModel();
                    ViewModelobj = (BankingSystem_Common.Constants.IndexViewModel)TempData["Dataset"];


                    SetDataSet(ViewModelobj.Items);

                    return PartialView("_TableLayoutWithDetailButtonView", ViewModelobj);
                }
                return PartialView("_TableLayoutWithDetailButtonView", null);
            }
            catch (Exception ex)
            {
               // (new CreateLogFiles()).ErrorLogfile(ex, Server.MapPath("~/Logs/ErrorLog"));
                return PartialView("_TableLayoutWithDetailButtonView", null);
            }
        }

        public void SetDataSet(DataSet ds)
        {
            Obj = ds;
        }

        [HttpPost]
        public JsonResult SetArray(List<string> list)
        {

            checkIdArr = list;

            return null;
        }

        //get all checked item 
        public List<string> getAllCheckList()
        {
            List<string> strlist = checkIdArr;

            return strlist;
        }
    }
}