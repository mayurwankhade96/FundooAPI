using CommonLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interfaces;
using RepositoryLayer.MSMQService;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
            try
            {
                user.Password = EncryptPassword(user.Password);
                _db.Users.Add(user);
                _db.SaveChanges();
                return true;
            }            
            catch(Exception)
            {
                throw new Exception("Email Already Registered!");
            }
        }

        public LoginResponse LoginUser(string email, string password)
        {
            var authUser = _db.Users.SingleOrDefault(x => x.Email == email && x.Password == password);

            if (authUser == null)
            {
                return null;
            }            

            LoginResponse loginResponse = new LoginResponse();

            loginResponse.Token = GenerateToken(authUser.Email, authUser.Id);
            loginResponse.Id = authUser.Id;
            loginResponse.FirstName = authUser.FirstName;
            loginResponse.LastName = authUser.LastName;
            loginResponse.Email = authUser.Email;
            return loginResponse;
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

        public bool ResetPassword(ResetPassword reset)
        {
            try
            {
                string encryptedPassword = EncryptPassword(reset.NewPassword);
                var user = _db.Users.SingleOrDefault(x => x.Email == reset.Email);

                if (user != null)
                {
                    user.Password = encryptedPassword;
                    _db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw new Exception("Email not found!");
            }
        }

        public bool ForgetPassword(string email)
        {
            try
            {
                string user;
                //string mailSubject = "Fundoo notes account password reset";
                //var userVerification = this._db.Users.SingleOrDefault(x => x.Email == email);
                var usr = this._db.Users.SingleOrDefault(x => x.Email == email);

                if (usr != null)
                {
                    string token = GenerateToken(usr.Email, usr.Id);
                    MSMQUtility msmq = new MSMQUtility();
                    msmq.SendMessage(email, token);

                    //var messageBody = msmq.ReceiveMessage();
                    //user = messageBody;
                    //using (MailMessage mailMessage = new MailMessage("mayur.wankhade2@gmail.com", email))
                    //{
                    //    mailMessage.Subject = mailSubject;
                    //    mailMessage.Body = user;
                    //    mailMessage.IsBodyHtml = true;
                    //    SmtpClient smtp = new SmtpClient();
                    //    smtp.Host = "smtp.gmail.com";
                    //    smtp.EnableSsl = true;
                    //    smtp.UseDefaultCredentials = false;
                    //    smtp.Credentials = new NetworkCredential("mayur.wankhade2@gmail.com", "khikhikhi");
                    //    smtp.Port = 587;
                    //    smtp.Send(mailMessage);
                    //}
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public User GetUser(string email)
        {
            var user = _db.Users.FirstOrDefault(x => x.Email == email);

            if(user != null)
            {
                return user;
            }
            return null;
        }
    }
}
