using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingSystemWebsite.Models.BankAccount
{
    public class BankAccountObject
    {
        public string BankAccountId { get; set; }
        public string CustomerType { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Nadtionaly { get; set; }
        public string IDNumber { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public decimal MonthlyIncome { get; set; }
        public decimal BalanceAmount { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string Address { get; set; }
        public string WayName { get; set; }
        public float ReceiveableAmount { get; set; }
        public float PayableAmount { get; set; }
        public string Email { get; set; }
        public string Discount_Percent { get; set; }
        public string BankAccountName { get; set; }
        public string Password { get; set; }


        public BankAccountObject(string v1, string v2, string v3, string v4, float v5, float v6)
        {
            this.BankAccountId = v1;
            this.Name = v2;
            this.CompanyName = v3;
            this.Phone = v4;
            this.ReceiveableAmount = v5;
            this.PayableAmount = v6;
        }

        public BankAccountObject()
        {
        }

    }
}