using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Policy;
using System.Web;
using System.Web.Http.Routing;
using System.Web.Routing;

namespace FinalAssignment.Models
{
    public partial class Post
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
                Ref = "GetPosts",
                Method = "GET",
                Url = baseUrl+posts
            });

            links.Add(new Link()
            {
                Ref = "GetPost",
                Method = "GET",
                Url = baseUrl + posts + this.id
            });

            links.Add(new Link()
            {
                Ref = "UpdatePost",
                Method = "PUT",
                Url = baseUrl + posts + this.id
            });

            links.Add(new Link()
            {
                Ref = "DeletePosts",
                Method = "DELETE",
                Url = baseUrl + posts + this.id
            });

            links.Add(new Link()
            {
                Ref = "GetPostComments",
                Method = "GET",
                Url = baseUrl + posts + this.id + comments
            });

            links.Add(new Link()
            {
                Ref = "AddPostComment",
                Method = "POST",
                Url = baseUrl + posts + this.id + comments
            });

            return links;
        }
    }
}