using System.Collections.Generic;
using trailAPI.Models;

namespace trailAPI.Services
{
    public class UserService
    {
        private readonly List<User> _users = new List<User>
        {
            new User { Email = "testme@test.com", Password = "insecurePWD" },
            new User { Email = "example@example.com", Password = "examplePWD" }
        };

        public IEnumerable<User> GetUsers()
        {
            return _users;
        }

        public bool ValidateUser(User usr)
        {
            return _users.Exists(u => u.Email == usr.Email && u.Password == usr.Password);
        }
    }
}