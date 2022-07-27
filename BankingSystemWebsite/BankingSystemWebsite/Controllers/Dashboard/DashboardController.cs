using BankingSystem_BL.Dashboard;
using BankingSystem_BL.Transaction;
using BankingSystem_Common.Constants;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankingSystemWebsite.Controllers.Dashboard
{
    public class DashboardController : Controller
    {
        string usertype = "";
        public static DataSet obj = null;
        public static DataSet Use_obj = null;
        public static List<string> list = new List<string>();
        public List<string> todolist;
        public List<string> tochecklist = new List<string>();
        public static BankingSystem_Common.Constants.IndexViewModel ViewModelobj;

        // GET: Dashboard
        public ActionResult dashboard()
        {

            string name = Convert.ToString(Session["UserName"]);

            if (name == null || name == "" || name == " ")
            {
                return RedirectToAction("login", "Login");
            }

            usertype = Convert.ToString(Session["LoginUserType"]).Trim();
            AddToObj(TransactionBL.SelectAllTransaction(name, usertype));

            DataTable dt = new DataTable();

            dt = DashboardBL.GetDashBoarddataDT(name, usertype);


            //TempData["DashBoadData"] = dt;
            Session["DashBoadData"] = dt;
            return View(ViewModelobj);
        }

        public void AddToObj(DataSet ds)
        {
            Use_obj = ds;

            //copy dataset without affecting ds
            obj = ds.Copy();


            Session["Dataset1"] = Use_obj;

            ViewModelobj = getModel(1);

            TempData["Dataset"] = ViewModelobj;
            //store dataset tempolarily to use in next page

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
                controller = "Dashboard"
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
                // NULLchecker = true;
            }
            else
            {
                newds.Tables.Add(new DataTable());
                // NULLchecker = false;
            }
            return newds;
        }



    }
}