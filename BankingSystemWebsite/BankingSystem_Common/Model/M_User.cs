using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem_Common.Model
{
    public class M_User
    {
        public string User_ID { get; set; }
        public string User_AccName { get; set; }
        public string Way_Name { get; set; }
        public string Way_ID { get; set; }
        public string User_WayName { get; set; }
        public string User_Password { get; set; }
        public string User_Type { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
    }
}
