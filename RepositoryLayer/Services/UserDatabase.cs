using CommonLayer;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserDatabase : IUserRL
    {
        private FundooContext db;
        public UserDatabase(FundooContext _db)
        {
            this.db = _db;
        }

        //public bool DeleteUser(int id)
        //{
        //    var user = GetUser(id);
        //    if (user == null)
        //    {
        //        return false;
        //    }
        //    db.users.Remove(user);
        //    db.SaveChanges();
        //    return true;
        //}

        public List<User> GetAllUsers()
        {
            return db.users.ToList();
        }

        //public User GetUser(int id)
        //{
        //    return db.users.FirstOrDefault(x => x.Id == id);
        //}

        public User LoginUser(string email, string password)
        {
            return db.users.FirstOrDefault(x => x.Email == email && x.Password == password);            
        }

        public bool RegisterNewUser(User user)
        {
            db.users.Add(user);
            db.SaveChanges();
            return true;
        }

        //public List<User> UpdateUser(int id, User user)
        //{
        //    var getUserToUpdate = GetUser(id);
        //    getUserToUpdate.FirstName = user.FirstName;
        //    getUserToUpdate.LastName = user.LastName;
        //    getUserToUpdate.Password = user.Password;
        //    db.SaveChanges();
        //    return GetAllUsers();
        //}
    }
}
