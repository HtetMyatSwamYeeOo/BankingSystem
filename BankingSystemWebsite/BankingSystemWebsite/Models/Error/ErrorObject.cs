using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingSystemWebsite.Models.Error
{
    public class ErrorObject
    {

        public string usertype { get; set; }

        public string Error_Title { get; set; }

        public string Error_Controller_name { get; set; }

        public string Error_ActionResult_Method_name { get; set; }

        public string Error_Detail { get; set; }

        public string Error_Message { get; set; }

        public string Fix_Error { get; set; }




    }
}