using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IUserBL
    {        
        bool RegisterNewUser(RegisterUser user);        
        LoginResponse LoginUser(string email, string password);
        bool ForgetPassword(string email);
        bool ResetPassword(ResetPassword reset);
        string GenerateToken(string userEmail, int userId);
        User GetUser(string email);
    }
}
