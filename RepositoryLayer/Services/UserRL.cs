using CommonLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        private FundooContext _db;        
        private readonly string _secret;

        public UserRL(FundooContext db, IConfiguration config)
        {
            this._db = db;            
            this._secret = config.GetSection("AppSettings").GetSection("Key").Value;
        }      

        public bool RegisterNewUser(User user)
        {
            user.CreateDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            user.ModifiedDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            _db.users.Add(user);
            _db.SaveChanges();
            return true;
        }        

        public LoginResponse LoginUser(string email, string password)
        {
            var authUser = _db.users.SingleOrDefault(x => x.Email == email && x.Password == password);

            if (authUser == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, authUser.Email),
                    new Claim("userId", authUser.Id.ToString(), ClaimValueTypes.Integer)
                }),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            LoginResponse loginResponse = new LoginResponse();
            
            loginResponse.Token = tokenHandler.WriteToken(token);
            loginResponse.Id = authUser.Id;
            loginResponse.FirstName = authUser.FirstName;
            loginResponse.LastName = authUser.LastName;
            loginResponse.Email = authUser.Email;
            return loginResponse;
        }        
    }
}
