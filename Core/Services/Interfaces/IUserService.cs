using System.Collections.Generic;
using Core.Entities;

namespace Core.Services
{
    public interface IUserService
    {
        User GetById(string id);
        User GetByNameOrEmail(string nameOrEmail);
        IList<User> GetList();
        void Update(User user);
        string Add(User user);
    }
}