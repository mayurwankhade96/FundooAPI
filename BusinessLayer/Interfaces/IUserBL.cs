using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IUserBL
    {
        List<User> GetAllUsers();
        User GetUser(int id);
    }
}
