using FinalAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalAssignment.Repository
{
    public class CommentRepository : Repository<Comment>
    {
        public void DeleteByPost(int postId)
        {
            this.context.Set<Comment>().RemoveRange(this.context.Set<Comment>().Where(c => c.postId == postId));
            this.context.SaveChanges();
        }
    }
}