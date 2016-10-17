using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories
{
    public interface IUserRepository
    {
        User Get(string id);
        IList<User> Get(IList<string> ids);
        IList<string> Find();
        IList<string> Find(string nameOrEmail);
        void Update(User user);
        string Add(User user);
    }
}