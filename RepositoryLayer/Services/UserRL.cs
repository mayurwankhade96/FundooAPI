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
        
        /// <summary>
        /// This method encode your password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string EncryptPassword(string password)
        {
            byte[] encData = new byte[password.Length];
            encData = Encoding.UTF8.GetBytes(password);
            string encodedData = Convert.ToBase64String(encData);
            return encodedData;
        }

        /// <summary>
        /// This method decode your password
        /// </summary>
        /// <param name="encodedData"></param>
        /// <returns></returns>
        public string DecryptPassword(string encodedData)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            Decoder decoder = encoding.GetDecoder();
            byte[] toDecodeByte = Convert.FromBase64String(encodedData);
            int charCount = decoder.GetCharCount(toDecodeByte, 0, toDecodeByte.Length);
            char[] decodedChar = new char[charCount];
            decoder.GetChars(toDecodeByte, 0, toDecodeByte.Length, decodedChar, 0);
            string result = new String(decodedChar);
            return result;
        }

        public bool RegisterNewUser(User user)
        {            
            user.Password = EncryptPassword(user.Password);
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
