using System.Collections.Generic;
using Core.Classes;

namespace Core.Repositories
{
    public interface IUserRepository
    {
        User GetById(int id);
        User GetByNameOrEmail(string nameOrEmail);
        IList<User> GetList();
        bool Save(User user);
        int Add(User user);
    }
}