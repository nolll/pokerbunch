using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Tests.Common.Builders;

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
                new UserBuilder()
                    .WithId(Constants.UserIdA)
                    .WithUserName(Constants.UserNameA)
                    .WithEmail(Constants.UserEmailA)
                    .WithRealName(Constants.UserRealNameA)
                    .WithDisplayName(Constants.UserDisplayNameA)
                    .WithEncryptedPassword(Constants.UserEncryptedPasswordA)
                    .WithSalt(Constants.UserSaltA)
                    .Build(),
                new UserBuilder()
                    .WithId(Constants.UserIdB)
                    .WithUserName(Constants.UserNameB)
                    .WithEmail(Constants.UserEmailB)
                    .WithRealName(Constants.UserRealNameB)
                    .WithDisplayName(Constants.UserDisplayNameB)
                    .WithEncryptedPassword(Constants.UserEncryptedPasswordB)
                    .WithSalt(Constants.UserSaltB)
                    .WithGlobalRole(Role.Admin)
                    .Build(),
                new UserBuilder()
                    .WithId(Constants.UserIdC)
                    .WithUserName(Constants.UserNameC)
                    .WithEmail(Constants.UserEmailC)
                    .WithRealName(Constants.UserRealNameC)
                    .WithDisplayName(Constants.UserDisplayNameC)
                    .WithEncryptedPassword(Constants.UserEncryptedPasswordC)
                    .WithSalt(Constants.UserSaltC)
                    .Build(),
                new UserBuilder()
                    .WithId(Constants.UserIdD)
                    .WithUserName(Constants.UserNameD)
                    .WithEmail(Constants.UserEmailD)
                    .WithRealName(Constants.UserRealNameD)
                    .WithDisplayName(Constants.UserDisplayNameD)
                    .WithEncryptedPassword(Constants.UserEncryptedPasswordD)
                    .WithSalt(Constants.UserSaltD)
                    .Build()
            };
        }
    }
}