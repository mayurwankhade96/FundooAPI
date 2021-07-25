using FundooAPI.Models;
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
        public List<User> users = new List<User>()
        {
            new User { Id = 1, FirstName = "Mayur", LastName = "Wankhade", Email = "mayur.wankhade2@gmail.com", PhoneNumber = "8082494818"},
            new User { Id = 2, FirstName = "Harshal", LastName = "Kadlak", Email = "hkadlak8@gmail.com", PhoneNumber = "123456789"}
        };

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            return users;
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = users.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                return NotFound();
            }
            return user;
        }
    }
}
