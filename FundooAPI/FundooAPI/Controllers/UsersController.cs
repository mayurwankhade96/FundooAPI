using BusinessLayer.Interfaces;
using CommonLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserBL users;
        public UsersController(IUserBL _user)
        {
            this.users = _user;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            return users.GetAllUsers();
        }

        //[HttpGet("{id}")]
        //public ActionResult<User> GetUser(int id)
        //{
        //    var user = users.GetUser(id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return user;
        //}

        [HttpPost]
        public ActionResult<User> PostUser(User user)
        {
            if (users.RegisterNewUser(user))
            {
                return user;
            }
            return BadRequest();
        }

        //[HttpDelete("{id}")]
        //public ActionResult<IEnumerable<User>> DeleteUser(int id)
        //{
        //    if (users.DeleteUser(id))
        //    {
        //        return users.GetAllUsers();
        //    }
        //    return NotFound();
        //}

        //[HttpPut("{id}")]
        //public ActionResult<IEnumerable<User>> UpdateUser(int id, User user)
        //{
        //    var uUser = users.UpdateUser(id, user);
        //    if (uUser != null)
        //    {
        //        return Ok(uUser);
        //    }
        //    return NotFound();
        //}

        [HttpPost("login")]
        public ActionResult<IEnumerable<User>> LoginUser(Login login)
        {
            var user = users.LoginUser(login.Email, login.Password);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }
    }
}
