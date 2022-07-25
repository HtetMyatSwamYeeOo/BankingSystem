using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem_Common.Model
{
    public class M_BankAccount
    {
            public M_BankAccount()
            {
                //this.BILLS = new HashSet<M_Bill>();
                //this.INVOICES = new HashSet<M_Invoice>();
                //this.PURCHASES = new HashSet<M_Purchase>();
                //this.SALES = new HashSet<M_Sale>();
            }

        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Nadtionaly { get; set; }
        public string IDNumber { get; set; }
        public decimal MonthlyIncome { get; set; }
        public decimal BalanceAmount { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string Password { get; set; }

        public string BankAccount_ID { get; set; }
            public string BankAccount_PersonName { get; set; }
            public string BankAccount_Type { get; set; }
            public string BankAccount_CompanyName { get; set; }
            public string BankAccount_MobilePhone { get; set; }
            public string BankAccount_WorkPhone { get; set; }
            public string BankAccount_Address { get; set; }
            public string BankAccount_City { get; set; }
            public string BankAccount_Notes { get; set; }
            public string BankAccount_Email { get; set; }
            public string Way_Name { get; set; }
            public string Way_ID { get; set; }

            public string BankAccount_Discount_percent { get; set; }

            public Nullable<bool> BankAccount_IsActive { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
            public string UpdatedBy { get; set; }
            public Nullable<System.DateTime> UpdatedOn { get; set; }
            public string DeletedBy { get; set; }
            public Nullable<System.DateTime> DeletedOn { get; set; }

            //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
            //public virtual ICollection<M_Bill> BILLS { get; set; }
            //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
            //public virtual ICollection<M_Invoice> INVOICES { get; set; }
            //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
            //public virtual ICollection<M_Purchase> PURCHASES { get; set; }
            //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
            //public virtual ICollection<M_Sale> SALES { get; set; }

    }
}
