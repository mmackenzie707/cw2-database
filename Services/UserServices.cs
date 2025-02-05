using System.Collections.Generic;
using System.Linq;
using trailAPI.Data;
using trailAPI.Models;

namespace trailAPI.Services
{
    public class UserServices
    {
        private readonly ApplicationDbContext _context;

        public UserServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public bool ValidateUser(User usr)
        {
            return _context.Users.Any(u => u.Email == usr.Email && u.Password == usr.Password);
        }
    }
}