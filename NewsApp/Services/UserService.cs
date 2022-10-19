using Microsoft.AspNetCore.Identity;
using NewsApp.Data;
using NewsApp.Models;

namespace NewsApp.Services
{
    public class UserService: IUserService
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;

        public UserService(ApplicationDbContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public IEnumerable<User> GetUsers()
        {
            return _db.Users.ToList();   
        }

        public User GetUser(int id)
        {
            var user = _db.Users.Find(id);
            return user;
        }

        public User CreateUser(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
            return user;
        }

        public User UpdateUser(User user)
        {
            var existingUser = _db.Users.Where(u => u.Id == user.Id).FirstOrDefault();

            existingUser.Email = user.Email;
            existingUser.NormalizedEmail = _userManager.NormalizeEmail(user.Email);
            existingUser.UserName = user.UserName;
            existingUser.NormalizedUserName = _userManager.NormalizeName(user.UserName);

            _db.Update(existingUser);
            _db.SaveChanges();
            return existingUser;
        }

        public void DeleteUser(int id)
        {
            var user = GetUser(id); 
            _db.Users.Remove(user);
            _db.SaveChanges();
        }
    }
}
