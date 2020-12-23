using FinalAssignment.Models;
using FinalAssignment.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FinalAssignment.Controllers
{
    [RoutePrefix("api/posts")]
    [BasicAuthentication]
    public class PostController : ApiController
    {
        PostRepository pr = new PostRepository();
        CommentRepository cr = new CommentRepository();

        [Route("", Name = "GetPosts")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(pr.GetAll());
        }

        [Route("{id}", Name = "GetPost")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
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
            pr.Insert(post);
            string uri = Url.Link("GetPost", new { id = post.id });
            return Created(uri, post);
        }

        [Route("{id}", Name = "UpdatePost")]
        [HttpPut]
        public IHttpActionResult Put([FromUri] int id, [FromBody] Post post)
        {
            post.id = id;
            pr.Update(post);
            return Ok(post);
        }

        [Route("{id}", Name = "DeletePost")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            pr.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("{id}/comments", Name = "GetPostComments")]
        [HttpGet]
        public IHttpActionResult GetComments(int id)
        {
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
            var comment = cr.Get(commentId);
            if (comment == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            
            return Ok(comment);
        }

        [Route("{id}/comments", Name = "AddPostComment")]
        [HttpPost]
        public IHttpActionResult Post([FromUri] int id, [FromBody] Comment comment)
        {
            comment.postId = id;
            cr.Insert(comment);
            string uri = Url.Link("GetComment", new { id = id });
            return Created(uri, comment);
        }

        [Route("{id}/comments/{commentId}", Name ="UpdateComment")]
        [HttpPut]
        public IHttpActionResult Put([FromUri] int id, [FromUri] int commentId, [FromBody] Comment comment)
        {
            comment.postId = id;
            comment.id = commentId;
            cr.Update(comment);
            return Ok(comment);
        }

        [Route("{id}/comments/{commentId}", Name = "DeleteComment")]
        [HttpDelete]
        public IHttpActionResult DeleteComments(int id, int commentId)
        {
            cr.Delete(commentId);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
