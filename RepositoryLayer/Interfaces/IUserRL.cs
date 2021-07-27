using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        List<User> GetAllUsers();
        // User GetUser(int id);
        bool RegisterNewUser(User user);
        // bool DeleteUser(int id);
        // List<User> UpdateUser(int id, User user);
        User LoginUser(string email, string password);
    }
}
