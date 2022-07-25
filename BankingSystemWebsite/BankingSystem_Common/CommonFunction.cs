using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem_Common
{
    public class CommonFunction
    {
        public static string createTimeStamp(string prefix)
        {

            string str = prefix + "_" + DateTime.Now.ToString("ddMMyyyyHHmmss");


            return str;
        }
        public static string createTimeStamp1(string prefix)
        {

            string str = prefix + DateTime.Now.ToString("ddMMmmss");


            return str;
        }

    }
}
