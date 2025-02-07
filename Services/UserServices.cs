using System.Collections.Generic;
using System.Linq;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using trailAPI.Data;
using trailAPI.Models;

namespace trailAPI.Services
{
    public class UserServices
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly NotificationService _notificationService;

        public UserServices(ApplicationDbContext dbContext, NotificationService notificationService)
        {
            _dbContext = dbContext;
            _notificationService = notificationService;
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

            // Send notification
            _notificationService.SendEmail(user.Email, "User Added", "Your user information has been logged.");
        }

        public void UpdateUser(User user)
        {
            var existingUser = _dbContext.Users.Find(user.UserID);
            if (existingUser != null)
            {
                existingUser.Email = user.Email;
                existingUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                _dbContext.SaveChanges();

                // Send notification
                _notificationService.SendEmail(user.Email, "User Updated", "Your user information has been updated.");
            }
        }

        public void DeleteUser(int id)
        {
            var user = _dbContext.Users.Find(id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();

                // Send notification
                _notificationService.SendEmail(user.Email, "User Deleted", "Your user information has been deleted.");
            }
        }

        public bool ValidateUser(User usr)
        {
            // Static username and password for testing purposes
            const string staticUsername = "testuser";
            const string staticPassword = "testpassword";

            if (usr.Email == staticUsername && usr.Password == staticPassword)
            {
                return true;
            }

            var user = _dbContext.Users.FirstOrDefault(u => u.Email == usr.Email);
            if (user == null)
            {
                return false;
            }

            // Verify the hashed password
            return BCrypt.Net.BCrypt.Verify(usr.Password, user.Password);
        }

        public void AddExploration(Exploration exploration)
        {
            // Verify that the trailID exists in the Trail_Information table
            var trailExists = _dbContext.Trail_Information.Any(t => t.TrailID == exploration.TrailID);
            if (!trailExists)
            {
                throw new KeyNotFoundException("The specified trailID does not exist in the Trail_Information table.");
            }

            _dbContext.Explorations.Add(exploration);
            _dbContext.SaveChanges();

            // Send notification
            var user = _dbContext.Users.Find(exploration.UserID);
            if (user != null)
            {
                _notificationService.SendEmail(user.Email, "Exploration Added", "Your exploration information has been logged.");
            }
        }

        public IEnumerable<Exploration> GetExplorationsByUserId(int userId)
        {
            return _dbContext.Explorations
                .Where(e => e.UserID == userId)
                .Include(e => e.TrailInformation) // Include TrailInformation for each exploration
                .ToList();
        }

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

            // Verify that the trailID exists in the Trail_Information table
            var trailExists = _dbContext.Trail_Information.Any(t => t.TrailID == dto.TrailID);
            if (!trailExists)
            {
                throw new KeyNotFoundException("The specified trailID does not exist in the Trail_Information table.");
            }

            var exploration = new Exploration
            {
                UserID = user.UserID,
                TrailID = dto.TrailID, // Use the TrailID provided by the user
                CompletionDate = dto.CompletionDate,
                CompletionStatus = dto.CompletionStatus
            };

            _dbContext.Explorations.Add(exploration);
            _dbContext.SaveChanges();

            // Send notification
            _notificationService.SendEmail(user.Email, "User and Exploration Added", "Your user and exploration information has been logged.");
        }

        // New methods for user rights

        public User GetUserByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Email == email);
        }

        public void UpdateUserEmail(string oldEmail, string newEmail)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == oldEmail);
            if (user != null)
            {
                user.Email = newEmail;
                _dbContext.SaveChanges();

                // Send notification
                _notificationService.SendEmail(newEmail, "Email Updated", "Your email address has been updated.");
            }
        }

        public void DeleteUserByEmail(string email)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();

                // Send notification
                _notificationService.SendEmail(email, "Account Deleted", "Your account has been deleted.");
            }
        }
    }
}