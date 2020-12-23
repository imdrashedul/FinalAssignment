using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalAssignment.Models
{
    public partial class Comment
    {
        public List<Link> Links { get { return GenerateLinks(); } }
        private List<Link> GenerateLinks()
        {
            List<Link> links = new List<Link>();

            HttpContext context = HttpContext.Current;
            string baseUrl = context.Request.Url.Scheme + "://" + context.Request.Url.Authority + context.Request.ApplicationPath.TrimEnd('/') + '/';

            string posts = "api/posts/";
            string comments = "/comments/";

            links.Add(new Link()
            {
                Ref = "GetPost",
                Method = "GET",
                Url = baseUrl + posts + this.postId
            });

            links.Add(new Link()
            {
                Ref = "GetComment",
                Method = "GET",
                Url = baseUrl + posts + this.postId + comments + this.id
            });

            links.Add(new Link()
            {
                Ref = "UpdateComment",
                Method = "PUT",
                Url = baseUrl + posts + this.postId + comments + this.id
            });

            links.Add(new Link()
            {
                Ref = "DeleteComment",
                Method = "DELETE",
                Url = baseUrl + posts + this.postId + comments + this.id
            });

            return links;
        }
    }
}