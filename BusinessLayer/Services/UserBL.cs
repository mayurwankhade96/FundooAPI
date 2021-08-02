using BusinessLayer.Interfaces;
using CommonLayer;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        private IUserRL _user;
        public UserBL(IUserRL user)
        {
            this._user = user;
        }

        public bool ResetPassword(ResetPassword reset)
        {
            return this._user.ResetPassword(reset);
        }

        public LoginResponse LoginUser(string email, string password)
        {
            return this._user.LoginUser(email, password);
        }

        public bool RegisterNewUser(User user)
        {
            try
            {
                _user.RegisterNewUser(user);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }       
    }
}
