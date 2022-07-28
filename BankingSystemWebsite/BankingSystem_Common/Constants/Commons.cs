using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem_Common.Constants
{
    public class Commons
    {
            public static string _SALE = "Sale";
            public static string _PURCHASE = "Purchase";

            public static string _RETURN = "Return";

            public static string _SALE_RETURN = "SaleReturn";

            public static string _OLDSALE = "OldSale";
            public static string _NEWSALE = "Edit";

            public static string _OLDPURCHASE = "OldPurchase";
            public static string _NEWPURCHASE = "Edit";


            #region Prefix

            #region Item
            public static string ItemSizePrefix = "IS";
            public static string ItemTypePrefix = "IT";
            public static string ItemPrefix = "I";
            #endregion


            public static string BankAccountPrefix = "NL";
            public static string CategoryPrefix = "CA";
            public static string AS_id_Prefix = "AS_";
            public static string SR_id_Prefix = "SR_";

            public static string SaleOrderPrefix = "SO";
            public static string IssueItemPrefix = "II_";
            public static string SalePrefix = "S";
            public static string BillPaymentPrefix = "BP";
            public static string PurchaseOrderPrefix = "PO_";
            public static string InvoicePaymentPrefix = "IP";
            public static string ItemOptionPrefix = "IOP";
            public static string EXPENSESNO = "EXNO";
            public static string InvoicePrefix = "IV";
            public static string ItemAdjPrefix = "IA";
            public static string InvoiceNOPrefix = "IVNO";
            public static string PurchasePrefix = "P";
            public static string BillPrefix = "B";
            public static string BillNOPrefix = "BNO";
            #endregion

            #region status
            public static string adjAdd = "Adjust Add";
            public static string adjsub = "Adjust Subtract";
            public static string adjSale = "Sale";
            public static string adjPurchase = "Purchase";
            public static string adjReturn = "ReturnSale";
            #endregion

            #region General
            public static string PrinterName = "EPSON L850 Series";
            public static string UserID = "U";
            public static string _DEALER = "Dealer";
            public static string _SUBDEALER = "Subdealer";
            public static string _OUTLET = "Outlet";
            public static string _MARKETING = "Marketing";
            public static string _BUSLINE = "Busline";
            public static string _OEM = "Oem";


            #endregion


            #region Language


            #region English
            public static string Dashboard_en = "Dashboard";
            public static string Contacts_en = "";
            #endregion


            #region Unicode
            public static string Dashboard_un = "Dashboard";
            public static string Contacts_un = "ေရာင္းခ်သူ/၀ယ္ယူသူ အခ်က္အလက္မ်ား ";
            #endregion

            #region Zawgyi
            public static string Dashboard_zg = "Dashboard";
            public static string Contacts_zg = "ရောင်းချသူ/ဝယ်ယူသူ အချက်အလက်များ";
            #endregion

            #endregion


            public static string Member = "Member";
            public static string Admin = "Admin";
            public static string Sale = "Sale";
            public static string RefPrefix = "ref";





    }
}
