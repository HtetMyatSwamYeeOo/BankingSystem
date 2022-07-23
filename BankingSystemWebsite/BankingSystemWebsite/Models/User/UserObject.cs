
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingSystemWebsite.Models.User
{
    public class UserObject
    {
        public  string userID  { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool rememberMe { get; set; }
        public string userType { get; set; }
        public string Way_Name { get; set; }
        public string wayname { get; set; }


    }
}