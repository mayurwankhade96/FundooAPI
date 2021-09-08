using BusinessLayer.Interfaces;
using CommonLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooAPI.Controllers
{
    [EnableCors()]
    [Route("api/[controller]")]
    [ApiController] 
    public class UsersController : ControllerBase
    {
        private IUserBL _users;
        public UsersController(IUserBL user)
        {
            this._users = user;
        }                

        [HttpPost("register")]
        public ActionResult PostUser(RegisterUser user)
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

        private int GetIdFromToken()
        {
            return Convert.ToInt32(User.FindFirst(x => x.Type == "userId").Value);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("resetpassword")]
        public ActionResult ResetPassword(ResetPassword reset)
        {
            try
            {
                int userId = GetIdFromToken();
                _users.ResetPassword(reset, userId);
                return Ok(new { message = "Password reset successful" });
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("forgetpassword")]
        public ActionResult ForgetPassword([FromBody]FogetPassword fogetPassword)
        {
            try
            {
                _users.ForgetPassword(fogetPassword.Email);
                return Ok(new { message = "Link has been sent to given email id..." });
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
