using BusinessLayer.Interfaces;
using CommonLayer;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        private IUserRL user;
        public UserBL(IUserRL _user)
        {
            this.user = _user;
        }
        public List<User> GetAllUsers()
        {
            return this.user.GetAllUsers();
        }

        public User GetUser(int id)
        {
            return this.user.GetUser(id);
        }
    }
}
