using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingSystemWebsite.Models.Transaction
{
    public class NewTransactionObject
    {
        public string trxno { get; set; }
        public string user_IBANo { get; set; }
        public string From_IBANo { get; set; }
        public string To_IBANo { get; set; }
        public string acccode { get; set; }
        public string trxtype { get; set; }
        public DateTime trxdate { get; set; }
        public string desc { get; set; }
        public decimal amount { get; set; }
        public decimal servicefeeamt { get; set; }
        public decimal totalamount { get; set; }

        public string currcode { get; set; }
        public decimal currate { get; set; }
        public string remark { get; set; }
        public string username { get; set; }
        public string customeridno { get; set; }
        public string bankname { get; set; }

    }
}