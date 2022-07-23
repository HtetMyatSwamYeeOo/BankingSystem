using BankingSystem_Common.Model;
using BankingSystem_DA.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem_BL.User
{
    public class UserBL
    {
        public static bool SearchNameAndPassword(string name, string password)
        {
            return UserDA.SearchNameAndPassword(name, password);
        }
        public static bool DeleteUserByID(string id, string deleteby)
        {
            return UserDA.DeleteUserByID(id, deleteby);
        }



        public static string searchUserIDWithUserName(string name)
        {
            return UserDA.searchUserIDWithUserName(name);

        }
        public static bool InsertUser(M_User user)
        {
            return UserDA.InsertUser(user);
        }
        public static DataSet SelectAllUsers()
        {
            return UserDA.SelectAllUsers();
        }
        public static bool UpdateUser(M_User user)
        {
            return UserDA.UpdateUser(user);
        }

        public static M_User SearchUser(string name, string password)
        {
            return UserDA.SearchUser(name, password);
        }


        public static string searchUserWithUserName(string name)
        {
            return UserDA.searchUserWithUserName(name);
        }

        public static object searchUserTypeWithUserID(string loginUserID)
        {
            return UserDA.searchUserTypeWithUserID(loginUserID);
        }


        public static string searchUserSaleAssignIDForToday(string LoginUserID)
        {
            return UserDA.searchUserSaleAssignIDForToday(LoginUserID);
        }


        public static M_User searchUserDetailWithUserName(string l)
        {
            return UserDA.searchUserDetailWithUserName(l);
        }

    }

}
