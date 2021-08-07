using BusinessLayer.Interfaces;
using CommonLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interfaces;
using RepositoryLayer.MSMQService;
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
        private readonly string _secret;
        public UserBL(IUserRL user, IConfiguration config)
        {
            this._user = user;
            this._secret = config.GetSection("AppSettings").GetSection("Key").Value;
        }                

        public bool ResetPassword(ResetPassword reset)
        {
            try
            {
                return this._user.ResetPassword(reset);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public LoginResponse LoginUser(string email, string password)
        {
            return this._user.LoginUser(email, password);
        }

        public bool RegisterNewUser(RegisterUser user)
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

        public bool ForgetPassword(string email)
        {
            try
            {
                return this._user.ForgetPassword(email);
            }
            catch (Exception)
            {
                throw;
            }            
        }
        
        public string GenerateToken(string userEmail, int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, userEmail),
                    new Claim("userId", userId.ToString(), ClaimValueTypes.Integer)
                }),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;
        }

        public User GetUser(string email)
        {
            return _user.GetUser(email);
        }
    }
}
