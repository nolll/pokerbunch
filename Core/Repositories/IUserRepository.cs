using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories
{
    public interface IUserRepository
    {
        User GetById(string id);
        IList<User> Get(IList<string> ids);
        IList<User> List();
        User GetByNameOrEmail(string nameOrEmail);
        void Update(User user);
        string Add(User user);
    }
}