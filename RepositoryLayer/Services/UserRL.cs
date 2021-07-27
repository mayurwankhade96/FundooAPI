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
            new User { Id = 1, FirstName = "Mayur", LastName = "Wankhade", Email = "mayur.wankhade2@gmail.com", Password = "789456"},
            new User { Id = 2, FirstName = "Sohail", LastName = "Qureshi", Email = "sohail123@gmail.com", Password = "456789"}
        };

        public bool DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllUsers()
        {
            return users;
        }

        public User GetUser(int id)
        {
            return users.FirstOrDefault(x => x.Id == id);
        }

        public bool RegisterNewUser(User user)
        {
            users.Add(user);
            return true;
        }

        public List<User> UpdateUser(int id, User user)
        {
            throw new NotImplementedException();
        }

        public User LoginUser(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
