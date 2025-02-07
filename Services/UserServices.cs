using System.Collections.Generic;
using System.Linq;
using BCrypt.Net;
using trailAPI.Data;
using trailAPI.Models;

namespace trailAPI.Services
{
    public class UserServices
    {
        private readonly ApplicationDbContext _dbContext;

        public UserServices(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<User> GetUsers()
        {
            return _dbContext.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return _dbContext.Users.Find(id);
        }

        public void AddUser(User user)
        {
            // Hash the password before saving the user
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            var existingUser = _dbContext.Users.Find(user.UserID);
            if (existingUser != null)
            {
                existingUser.Email = user.Email;
                existingUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                _dbContext.SaveChanges();
            }
        }

        public void DeleteUser(int id)
        {
            var user = _dbContext.Users.Find(id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
            }
        }

        public bool ValidateUser(User usr)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == usr.Email);
            if (user == null)
            {
                return false;
            }

            // Verify the hashed password
            return BCrypt.Net.BCrypt.Verify(usr.Password, user.Password);
        }

        // Add the AddExploration method
        public void AddExploration(Exploration exploration)
        {
            _dbContext.Explorations.Add(exploration);
            _dbContext.SaveChanges();
        }

        // Add the GetExplorationsByUserId method
        public IEnumerable<Exploration> GetExplorationsByUserId(int userId)
        {
            return _dbContext.Explorations.Where(e => e.UserID == userId).ToList();
        }

        // Add the AddUserWithExploration method
        public void AddUserWithExploration(UserExplorationDto dto)
        {
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword("defaultPassword") // Set a default password or handle password input
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            var exploration = new Exploration
            {
                UserID = user.UserID,
                TrailID = "AA0001", // Generate or set the TrailID as needed
                CompletionDate = dto.CompletionDate,
                CompletionStatus = dto.CompletionStatus
            };

            _dbContext.Explorations.Add(exploration);
            _dbContext.SaveChanges();
        }
    }
}