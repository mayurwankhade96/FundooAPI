using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        List<User> GetAllUsers();
        User GetUser(int id);
    }
}
