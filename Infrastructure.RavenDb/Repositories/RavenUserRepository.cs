using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;

namespace Infrastructure.RavenDb.Repositories
{
    public class RavenUserRepository : IUserRepository
    {
        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public User GetByNameOrEmail(string nameOrEmail)
        {
            throw new NotImplementedException();
        }

        public IList<User> GetList()
        {
            throw new NotImplementedException();
        }

        public bool Save(User user)
        {
            throw new NotImplementedException();
        }

        public int Add(User user)
        {
            throw new NotImplementedException();
        }
    }
}
