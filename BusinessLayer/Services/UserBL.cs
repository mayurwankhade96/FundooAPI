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

        //public bool DeleteUser(int id)
        //{
        //    user.DeleteUser(id);
        //    return true;
        //}

        public List<User> GetAllUsers()
        {
            return this.user.GetAllUsers();
        }

        //public User GetUser(int id)
        //{
        //    return this.user.GetUser(id);
        //}

        public User LoginUser(string email, string password)
        {
            return this.user.LoginUser(email, password);
        }

        public bool RegisterNewUser(User usr)
        {
            user.RegisterNewUser(usr);
            return true;
        }

        //public List<User> UpdateUser(int id, User user)
        //{
        //    return this.user.UpdateUser(id, user);
        //}
    }
}
