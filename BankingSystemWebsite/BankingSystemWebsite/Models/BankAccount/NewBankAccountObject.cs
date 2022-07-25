using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankingSystemWebsite.Models.BankAccount
{
    public class NewBankAccountObject
    {

        public string BankAccountId { get; set; }

        [Required(ErrorMessage = "Enter Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Enter Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Enter Nadtionaly")]
        public string Nadtionaly { get; set; }

        [Required(ErrorMessage = "Enter IDNumber")]
        public string IDNumber { get; set; }
        public decimal MonthlyIncome { get; set; }
        public decimal BalanceAmount { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }

        [Required(ErrorMessage = "Please Select Type")]
        [Display(Name = "Type ")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Plese Put User Name")]
        [Display(Name = "User name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Company Name is required")]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Entet Phone Number")]
        [Display(Name = "BankAccount Phone")]
        public string BankAccountPhone { get; set; }

        [Required(ErrorMessage = "Enter Work Phone Number")]
        [Display(Name = "Work Phone")]
        public string Workphone { get; set; }

        [Required(ErrorMessage = "Enter Address")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Enter Street")]
        [Display(Name = "Street")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Enter City")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Note")]
        public string Note { get; set; }

        [Required(ErrorMessage = "Please Enter Email Address")]
        [Display(Name = "Email")]

        [EmailAddress(ErrorMessage = "Enter Correct Email Address")]
        public string Email { get; set; }

        public string Password { get; set; }


        [Required(ErrorMessage = "Enter Discount Percent")]
        [Display(Name = "Discount Percent")]
        public string Discount_Percent { get; set; }

        [Required(ErrorMessage = "Enter Way Name")]
        [Display(Name = "Enter Way Names")]
        public string Way_Name { get; set; }
        public string Way_ID { get; set; }



        public NewBankAccountObject(string v1, string v2, string v3, string v4, string v5, string v6, string v7, string v8, string v9, string v10, string v11)
        {
            /*
             Block for customer type only
            this.Type = v1;
            */
            v1 = "bankaccount";
            this.Type = v1;
            this.Name = v2;
            this.CompanyName = v3;
            this.BankAccountPhone = v4;
            this.Workphone = v5;
            this.Address = v6;
            this.Street = v7;
            this.City = v8;
            this.Note = v9;
            this.Email = v10;
            this.Discount_Percent = v11;
            this.IDNumber = "";
            this.Nadtionaly = "";
        }

        public NewBankAccountObject()
        {
        }


    }
}