using BankingSystem_Common.Model;
using BankingSystem_DA.Transaction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem_BL.Transaction
{
    public class TransactionBL
    {
        public static bool InsertNewTransaction(M_Transaction mc)
        {
            return TransactionDA.InsertNewTransaction(mc);
        }

        public static DataSet SelectAllTransaction(string userid, string usertype)
        {

            return TransactionDA.SelectAllBankAccount(userid,usertype);
        }

        public static List<M_Transaction> SelectAllTransactionName()
        {
            throw new NotImplementedException();
        }

        public static DataSet SearchTransaction(string name)
        {
            throw new NotImplementedException();
        }
    }
}
