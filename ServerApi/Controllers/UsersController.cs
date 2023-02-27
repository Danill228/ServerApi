using Microsoft.AspNetCore.Mvc;
using ServerBelPodryad.Models;
using ServerBelPodryad.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            List<User> listUser = UserStore.GetAllUsers();
            return listUser;

        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            try
            {
                User user = UserStore.GetUserById(id);
                if (user.Id == 0)
                {
                    return NotFound();
                }

                return user;
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("authorization")]
        public async Task<ActionResult<User>> Get([FromForm] User user)
        {
            if (user.Login == null || user.Password == null)
            {
                return StatusCode(412);
            }

            User findUser = UserStore.GetUserByLoginAndPassword(user.Login, user.Password);
            if (findUser.Id == 0)
            {
                return StatusCode(401);
            }

            return findUser;
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult<User>> Post([FromForm] User user)
        {
            if (user.Login == null || user.Password == null)
            {
                return StatusCode(412);
            }

            if (UserStore.IsUserExists(user.Login))
            {
                return StatusCode(409, "already exists");
            }

            UserStore.CreateUser(user.Login, user.Password, 2);

            return Ok();
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(int id, [FromBody] User user)
        {
            User findUser = UserStore.GetUserById(id);
            if (findUser.Id == 0)
            {
                return NotFound();
            }
            user.Id = id;

            UserStore.UpdateUser(user);

            return Ok();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            UserStore.DeleteUserById(id);
            return Ok();
        }
    }
}
