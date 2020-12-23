using FinalAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalAssignment.Repository
{
    public class PostRepository : Repository<Post>
    {
        protected CommentRepository cr = new CommentRepository();

        public List<Comment> GetComments(int id)
        {
            return cr.GetAll().Where(c => c.postId==id).ToList();
        }
    }
}