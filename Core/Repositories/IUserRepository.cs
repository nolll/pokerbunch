using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories
{
    public interface IUserRepository
    {
        User Get(int id);
        IList<User> Get(IList<int> ids);
        IList<int> Search();
        IList<int> Search(string nameOrEmail);
        bool Save(User user);
        int Add(User user);
    }
}