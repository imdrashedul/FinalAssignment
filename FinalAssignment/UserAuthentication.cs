using FinalAssignment.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalAssignment
{
    public class UserAuthentication
    {
        public static bool Verify(string username, string password)
        {
            UserRepository ur = new UserRepository();
            return ur.getUser(username, password) != null;
        }
    }
}