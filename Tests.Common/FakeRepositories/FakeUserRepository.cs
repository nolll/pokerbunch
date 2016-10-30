using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;

namespace Tests.Common.FakeRepositories
{
    public class FakeUserRepository : IUserRepository
    {
        public User Added { get; private set; }
        public User Saved { get; private set; }
        private readonly IList<User> _list;

        public FakeUserRepository()
        {
            _list = CreateList();
        }

        public User GetById(string id)
        {
            return _list.FirstOrDefault(o => o.Id == id);
        }

        public IList<User> Get(IList<string> ids)
        {
            return _list.Where(o => ids.Contains(o.Id)).ToList();
        }

        public IList<User> List()
        {
            return _list;
        }

        public User GetByNameOrEmail(string nameOrEmail)
        {
            return _list.FirstOrDefault(o => o.UserName == nameOrEmail || o.Email == nameOrEmail);
        }

        public string Add(User user)
        {
            Added = user;
            return "1";
        }

        public void Update(User user)
        {
            Saved = user;
        }

        private IList<User> CreateList()
        {
            return new List<User>
            {
                TestData.UserA,
                TestData.UserB,
                TestData.UserC,
                TestData.UserD
            };
        }
    }
}