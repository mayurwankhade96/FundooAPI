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

        [HttpPost("Register")]
        public ActionResult PostUser(User user)
        {
            try
            {
                _users.RegisterNewUser(user);
                return Ok(new { success = true, message = "User Registered successfully", user.Email, user.FirstName, user.LastName });                
            }
            catch(Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }                                    
        }               

        [HttpPost("Login")]
        public ActionResult LoginUser(Login login)
        {
            var user = _users.LoginUser(login.Email, login.Password);

            if (user != null)
            {
                return Ok(user);                                
            }
            return BadRequest(new { message = "Email or Password is incorrect" });
        }

        [HttpPut("reset")]
        public ActionResult ResetPassword(ResetPassword reset)
        {
            var user = _users.ResetPassword(reset);

            if(user == true)
            {
                return Ok(new { message = "Password reset successful", Data = reset.Email });
            }
            return BadRequest(new { message = "Email dosen't exist" });
        }
    }
}
