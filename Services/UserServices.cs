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

        public void AddUserWithExploration(UserWithExploration user)
        {
            // Hash the password before saving the user
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _dbContext.UsersWithExplorations.Add(user);
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

        public User ValidateUser(string username, string password)
        {
            // Static username and password for testing purposes
            const string staticUsername = "testuser@test.com";
            const string staticPassword = "testpassword";

            if (username == staticUsername && password == staticPassword)
            {
                return new User
                {
                    Email = staticUsername,
                    Password = staticPassword,
                    FirstName = "Static",
                    LastName = "User"
                };
            }

            var user = _dbContext.Users.FirstOrDefault(u => u.Email == username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null;
            }

            return user;
        }

        // Add the GetUserByEmail method
        public User GetUserByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Email == email);
        }

        // Add the UpdateUserEmail method
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

        // Add the DeleteUserByEmail method
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