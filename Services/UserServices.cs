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

            // Add authorization entry
            var authorization = new IsAuthorized
            {
                UserID = user.UserID,
                IsAuthorizedUser = true
            };
            _dbContext.IsAuthorized.Add(authorization);
            _dbContext.SaveChanges();

            // Send notification
            _notificationService.SendEmail(user.Email, "User Added", "Your user information has been logged.");
        }

        public void AddAdminUser(User user)
        {
            // Hash the password before saving the user
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.IsAdmin = true;
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            // Add authorization entry
            var authorization = new IsAuthorized
            {
                UserID = user.UserID,
                IsAuthorizedUser = true
            };
            _dbContext.IsAuthorized.Add(authorization);
            _dbContext.SaveChanges();

            // Send notification
            _notificationService.SendEmail(user.Email, "Admin User Added", "Your admin user information has been logged.");
        }

        public void UpdateUser(User user)
        {
            var existingUser = _dbContext.Users.FirstOrDefault(u => u.UserID == user.UserID);
            if (existingUser != null)
            {
                existingUser.Email = user.Email;
                existingUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName; // Ensure this property is updated
                existingUser.IsAdmin = user.IsAdmin;

                _dbContext.Users.Update(existingUser); // Ensure the entity is marked as modified
                _dbContext.SaveChanges(); // Save changes to the database
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

        public User ValidateUser(string email, string password)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null;
            }

            var isAuthorized = _dbContext.IsAuthorized.FirstOrDefault(ia => ia.UserID == user.UserID);
            if (isAuthorized == null || !isAuthorized.IsAuthorizedUser)
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