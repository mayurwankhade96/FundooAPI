using CommonLayer;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        public List<User> users = new List<User>()
        {
            new User { Id = 1, FirstName = "Mayur", LastName = "Wankhade", Email = "mayur.wankhade2@gmail.com", PhoneNumber = "8082494818"},
            new User { Id = 2, FirstName = "Sohail", LastName = "Qureshi", Email = "sohail123@gmail.com", PhoneNumber = "123456789"}
        };

        public List<User> GetAllUsers()
        {
            return users;
        }

        public User GetUser(int id)
        {
            return users.FirstOrDefault(x => x.Id == id);
        }
    }
}
