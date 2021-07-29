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
        private IUserBL _users;
        public UsersController(IUserBL user)
        {
            this._users = user;
        }                

        [HttpPost]
        public ActionResult PostUser(User user)
        {
            if (_users.RegisterNewUser(user))
            {
                return Ok(user);
            }
            return BadRequest();
        }               

        [HttpPost("login")]
        public ActionResult LoginUser(Login login)
        {
            var user = _users.LoginUser(login.Email, login.Password);

            if (user != null)
            {
                return Ok(user);                                
            }
            return BadRequest(new { message = "Email or Password is incorrect" });
        }
    }
}
