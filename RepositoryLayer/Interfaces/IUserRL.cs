using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {        
        bool RegisterNewUser(RegisterUser user);        
        LoginResponse LoginUser(string email, string password);
        bool ForgetPassword(string email);
        bool ResetPassword(ResetPassword reset, int userId);
        string GenerateToken(string userEmail, int userId);
        User GetUser(string email);
    }
}
