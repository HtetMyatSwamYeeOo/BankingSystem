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
            Random rnd = new Random();
            string randomnumber = rnd.Next().ToString();
            string tworn = randomnumber.Substring(randomnumber.Length - 2);

            string str = prefix  + tworn + "ABNA" + DateTime.Now.ToString("ddMMyymmss");


            return str;
        }
        public static string createTimeStamp1(string prefix)
        {

            string str = prefix + DateTime.Now.ToString("ddMMmmss");


            return str;
        }

    }
}
