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
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        UserRepository ur = new UserRepository();

        [Route("{id}", Name = "GetUser")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(ur.Get(id));
        }

        [Route("{username}/{password}", Name = "GetUserByUP")]
        [HttpGet]
        public IHttpActionResult Get(string username, string password)
        {
            return Ok(ur.getUser(username, password));
        }

        [Route("", Name = "Adduser")]
        [HttpPost]
        public IHttpActionResult Post([FromBody] UserMapper userMapped)
        {
            User exists = ur.getUser(userMapped.username);

            User user = new User();

            user.name = userMapped.name;
            user.username = userMapped.username;
            user.password = userMapped.password;

            if (exists == null)
            {
                ur.Insert(user);
                string uri = Url.Link("GetUser", new { id = user.id });
                return Created(uri, user);
            }

            return BadRequest("Username Already Taken");  
        }
    }
}
