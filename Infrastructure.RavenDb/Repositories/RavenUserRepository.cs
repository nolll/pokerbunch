using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Infrastructure.Storage;

namespace Infrastructure.RavenDb.Repositories
{
    public class RavenUserRepository : RavenRepository, IUserRepository
    {
        public User GetById(int id)
        {
            using (var session = GetSession())
            {
                var rawUser = session.Load<RawUser>(RawUser.ToStringId(id));
                return RawUser.CreateReal(rawUser);
            }
        }

        public User GetByNameOrEmail(string nameOrEmail)
        {
            using (var session = GetSession())
            {
                var rawUsers = session.Query<RawUser>().Where(o => o.UserName == nameOrEmail || o.Email == nameOrEmail).ToList();
                return RawUser.CreateReal(rawUsers.FirstOrDefault());
            }
        }

        public IList<User> GetList()
        {
            using (var session = GetSession())
            {
                var rawUsers = session.Query<RawUser>().ToList();
                return rawUsers.Select(RawUser.CreateReal).ToList();
            }
        }

        public bool Save(User user)
        {
            using (var session = GetSession())
            {
                var rawUser = RawUser.Create(user);
                session.Store(rawUser);
             
                session.SaveChanges();
                return true;
            }
        }

        public int Add(User user)
        {
            using (var session = GetSession())
            {
                var rawUser = RawUser.Create(user);
                session.Store(rawUser);

                session.SaveChanges();
            }
            //todo: should return the generated id
            return 0;
        }
    }
}