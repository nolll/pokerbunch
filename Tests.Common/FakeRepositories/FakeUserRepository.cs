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

        public User GetById(int id)
        {
            return _list.FirstOrDefault(o => o.Id == id);
        }

        public User GetByNameOrEmail(string nameOrEmail)
        {
            return _list.FirstOrDefault(o => o.UserName == nameOrEmail || o.Email == nameOrEmail);
        }

        public IList<User> GetList()
        {
            return _list;
        }

        public int Add(User user)
        {
            Added = user;
            return 1;
        }

        public bool Save(User user)
        {
            Saved = user;
            return true;
        }

        private IList<User> CreateList()
        {
            return new List<User>
            {
                new User(Constants.UserIdA, Constants.UserNameA, Constants.UserDisplayNameA, Constants.UserRealNameA, Constants.UserEmailA, Role.Player, Constants.UserEncryptedPasswordA, Constants.UserSaltA),
                new User(Constants.UserIdB, Constants.UserNameB, Constants.UserDisplayNameB, Constants.UserRealNameB, Constants.UserEmailB, Role.Admin, Constants.UserEncryptedPasswordB, Constants.UserSaltB),
                new User(Constants.UserIdC, Constants.UserNameC, Constants.UserDisplayNameC, Constants.UserRealNameC, Constants.UserEmailC, Role.Player, Constants.UserEncryptedPasswordC, Constants.UserSaltC),
                new User(Constants.UserIdD, Constants.UserNameD, Constants.UserDisplayNameD, Constants.UserRealNameD, Constants.UserEmailD, Role.Player, Constants.UserEncryptedPasswordD, Constants.UserSaltD),
            };
        }
    }
}