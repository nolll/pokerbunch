using System.Collections.Generic;
using Core.Classes;

namespace Core.Repositories
{
    public interface IUserRepository
    {
        User GetUserById(int id);
        User GetUserByToken(string token);
        User GetUserByNameOrEmail(string userNameOrEmail);
        IList<User> GetAll();
        bool UpdateUser(User user);
        int AddUser(User user);
    }
}