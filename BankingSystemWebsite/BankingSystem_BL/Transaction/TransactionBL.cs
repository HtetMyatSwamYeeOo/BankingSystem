using BankingSystem_Common.Model;
using BankingSystem_DA.Transaction;
using System;
using System.Collections.Generic;
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
    }
}
