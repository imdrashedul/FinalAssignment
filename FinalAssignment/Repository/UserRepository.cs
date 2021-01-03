using FinalAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalAssignment.Repository
{
    public class UserRepository : Repository<User>
    {
        public User getUser(string username)
        {
            return this.context.Set<User>().Where(u => u.username == username).FirstOrDefault();
        }

        public User getUser(string username, string password)
        {
            return this.context.Set<User>().Where(u => u.username == username && u.password==password).FirstOrDefault();
        }
    }
}