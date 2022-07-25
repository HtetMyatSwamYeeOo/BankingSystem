using BankingSystem_Common.Model;
using BankingSystem_DA.BankAccount;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem_BL.BankAccount
{
    public class BankAccountBL
    {
            public static DataSet SelectAllBankAccount()
            {

                return BankAccountDA.SelectAllBankAccount();
            }
            public static DataSet SelectAllBankAccountByDealer()
            {
                return BankAccountDA.SelectAllBankAccountByDealer();
            }
            public static DataSet SelectAllBankAccountBySubDealer()
            {
                return BankAccountDA.SelectAllBankAccountBySubDealer();
            }
            public static DataSet SelectAllBankAccountByOutlet()
            {
                return BankAccountDA.SelectAllBankAccountByOutlet();
            }
            public static DataSet SelectAllBankAccountByOem()
            {
                return BankAccountDA.SelectAllBankAccountByOem();
            }
            public static DataSet SelectAllBankAccountByMarketing()
            {
                return BankAccountDA.SelectAllBankAccountByMarketing();
            }
            public static DataSet SelectAllBankAccountByBusline()
            {
                return BankAccountDA.SelectAllBankAccountByBusline();
            }

            public static List<M_BankAccount> SearchAllWayName(string searchKey)
            {
                return BankAccountDA.SearchAllWayName(searchKey);
            }

            public static List<M_BankAccount> SelectAllBankAccountListByCustomerType(string type)
            {
                return BankAccountDA.SelectAllBankAccountListByCustomerType(type);
            }




            public static Boolean InsertBankAccount(M_BankAccount BankAccount)
            {
                return BankAccountDA.InsertBankAccount(BankAccount);
            }//NN

            public static Boolean UpdateBankAccountById(M_BankAccount BankAccount)
            {
                return BankAccountDA.UpdateBankAccountById(BankAccount);
            }

            public static Boolean DeleteBankAccountByID(string BankAccountID, string DeletedBy)
            {
                return BankAccountDA.DeleteBankAccountByID(BankAccountID, DeletedBy);
            }

            public static Boolean UpdateBankAccountStatusByID(string BankAccount_ID, bool BankAccount_IsActive, string updatedby)
            {
                return BankAccountDA.UpdateBankAccountStatusByID(BankAccount_ID, BankAccount_IsActive, updatedby);
            }

            public static M_BankAccount SelectAllBankAccountBYID(string id)
            {
                return BankAccountDA.SelectAllBankAccountBYID(id);
            }
            public static M_BankAccount SelectCustomerIDbyName(string name)
            {
                return BankAccountDA.SelectCustomerIDbyName(name);
            }

            public static DataSet SelectAllTransactionBYBankAccountID(string id)
            {
                return BankAccountDA.SelectAllTransactionBYBankAccountID(id);
            }

            public static string checkWayNameisAlreadyExitorNot(string wayName)
            {
                return BankAccountDA.checkWayNameisAlreadyExitorNot(wayName);
            }

            public static bool InsertNewWaytoWayTable(string wayName, string Created_By)
            {
                return BankAccountDA.InsertNewWaytoWayTable(wayName, Created_By);
            }

            public static DataSet SelectAllTransactionBillBYBankAccountID(string id)
            {
                return BankAccountDA.SelectAllTransactionBillBYBankAccountID(id);
            }
            public static DataSet SearchBankAccount(string name)
            {
                return BankAccountDA.SearchBankAccount(name);
            }

        public static object SelectAllPersonalTypeBankAccount()
        {
            return BankAccountDA.SelectAllPersonalTypeBankAccount();
        }

        public static List<M_BankAccount> SelectAllBankAccountName()
            {
                return BankAccountDA.SelectAllBankAccountName();
            }

        public static object SelectAllBusinessTypeBankAccount()
        {
            return BankAccountDA.SelectAllBusinessTypeBankAccount();
        }

        public static List<string> SelectAllCompanyName()
            {
                return BankAccountDA.SelectAllCompanyName();
            }



    }
}
