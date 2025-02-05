using System.Collections.Generic;
using trailAPI.Models;

namespace trailAPI.Services
{
    public class UserServices
    {
        private readonly List<User> _users = new List<User>();

        public IEnumerable<User> GetUsers()
        {
            return _users;
        }

        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public bool ValidateUser(User usr)
        {
            return _users.Exists(u => u.Email == usr.Email && u.Password == usr.Password);
        }
    }
}