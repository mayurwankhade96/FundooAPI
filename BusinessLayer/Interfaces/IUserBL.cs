using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IUserBL
    {        
        bool RegisterNewUser(User user);        
        LoginResponse LoginUser(string email, string password);
        bool ResetPassword(ResetPassword reset);
    }
}
