using FinalAssignment.Models;
using FinalAssignment.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace FinalAssignment.Controllers
{
    [RoutePrefix("api/posts")]
    [BasicAuthentication]
    public class PostController : ApiController
    {
        [Route("", Name = "GetPosts")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            PostRepository pr = new PostRepository();
            return Ok(pr.GetAll().OrderByDescending(p => p.id));
        }

        [Route("{id}", Name = "GetPost")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            PostRepository pr = new PostRepository();
            var category = pr.Get(id);
            if (category == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(category);
        }

        [Route("", Name = "AddPost")]
        [HttpPost]
        public IHttpActionResult Post([FromBody] Post post)
        {
            PostRepository pr = new PostRepository();
            UserRepository ur = new UserRepository();

            User loggedUser = ur.getUser(Thread.CurrentPrincipal.Identity.Name);
            post.userId = loggedUser.id;
            
            pr.Insert(post);
            string uri = Url.Link("GetPost", new { id = post.id });
            return Created(uri, post);
        }

        [Route("{id}", Name = "UpdatePost")]
        [HttpPut]
        public IHttpActionResult Put([FromUri] int id, [FromBody] Post post)
        {
            PostRepository pr = new PostRepository();
            UserRepository ur = new UserRepository();

            User loggedUser = ur.getUser(Thread.CurrentPrincipal.Identity.Name);
            Post targetPost = pr.Get(id);

            if (targetPost.userId==loggedUser.id)
            {
                post.id = id;
                post.userId = targetPost.userId;
                pr = new PostRepository();
                pr.Update(post);
                return Ok(post);
            }

            return BadRequest("Not Authorized to Update this Post");
        }

        [Route("{id}", Name = "DeletePost")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            PostRepository pr = new PostRepository();
            UserRepository ur = new UserRepository();
            CommentRepository cr = new CommentRepository();
            User loggedUser = ur.getUser(Thread.CurrentPrincipal.Identity.Name);
            Post targetPost = pr.Get(id);

            if (targetPost.userId == loggedUser.id)
            {
                cr.DeleteByPost(id);
                pr.Delete(id);
                return StatusCode(HttpStatusCode.NoContent);
            }

            return BadRequest("Not Authorized to Delete this Post");
        }

        [Route("{id}/comments", Name = "GetPostComments")]
        [HttpGet]
        public IHttpActionResult GetComments(int id)
        {
            PostRepository pr = new PostRepository();
            var comments = pr.GetComments(id);
            if (comments == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            
            return Ok(comments);
        }

        [Route("{id}/comments/{commentId}", Name = "GetComment")]
        [HttpGet]
        public IHttpActionResult GetComment(int id, int commentId)
        {
            CommentRepository cr = new CommentRepository();
            Comment targetComment = cr.Get(commentId);

            if (targetComment.postId == id)
            {
                var comment = cr.Get(commentId);
                if (comment == null)
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }

                return Ok(comment);
            }
            return BadRequest("Invalid Post");
        }

        [Route("{id}/comments", Name = "AddPostComment")]
        [HttpPost]
        public IHttpActionResult Post([FromUri] int id, [FromBody] Comment comment)
        {
            CommentRepository cr = new CommentRepository();
            UserRepository ur = new UserRepository();
            User loggedUser = ur.getUser(Thread.CurrentPrincipal.Identity.Name);
            comment.postId = id;
            comment.userId = loggedUser.id;
            cr.Insert(comment);
            string uri = Url.Link("GetComment", new { id = id, commentId = comment.id });
            return Created(uri, comment);
        }

        [Route("{id}/comments/{commentId}", Name ="UpdateComment")]
        [HttpPut]
        public IHttpActionResult Put([FromUri] int id, [FromUri] int commentId, [FromBody] Comment comment)
        {
            CommentRepository cr = new CommentRepository();
            PostRepository pr = new PostRepository();
            UserRepository ur = new UserRepository();
            User loggedUser = ur.getUser(Thread.CurrentPrincipal.Identity.Name);
            Post targetPost = pr.Get(id);
            Comment targetComment = cr.Get(commentId);

            if(targetComment.postId==id)
            {
                if(targetComment.userId==loggedUser.id)
                {
                    comment.userId = targetComment.userId;
                    comment.postId = id;
                    comment.id = commentId;
                    cr = new CommentRepository();
                    cr.Update(comment);
                    return Ok(comment);
                }
                return BadRequest("Not Authorized to Update this Comment");
            }
            return BadRequest("Invalid Post");
        }

        [Route("{id}/comments/{commentId}", Name = "DeleteComment")]
        [HttpDelete]
        public IHttpActionResult DeleteComments(int id, int commentId)
        {
            CommentRepository cr = new CommentRepository();
            PostRepository pr = new PostRepository();
            UserRepository ur = new UserRepository();
            User loggedUser = ur.getUser(Thread.CurrentPrincipal.Identity.Name);
            Post targetPost = pr.Get(id);
            Comment targetComment = cr.Get(commentId);

            if (targetComment.postId == id)
            {
                if (targetComment.userId == loggedUser.id || targetPost.userId == loggedUser.id)
                {
                    cr.Delete(commentId);
                    return StatusCode(HttpStatusCode.NoContent);
                }
                return BadRequest("Not Authorized to Delete this Comment");
            }
            return BadRequest("Invalid Post");
            
        }
    }
}
