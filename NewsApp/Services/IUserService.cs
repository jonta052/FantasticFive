using NewsApp.Models;

namespace NewsApp.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();

        User GetUser(int id);

        User CreateUser(User user);

        User UpdateUser(User user); 

        void DeleteUser(int id);    
    }
}
