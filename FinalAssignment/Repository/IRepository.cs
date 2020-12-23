using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalAssignment.Repository
{
    interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T Get(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}