using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Infrastructure.Storage;

namespace Infrastructure.RavenDb.Repositories
{
    public class TempRavenUserRepository : RavenRepository, IRavenUserRepository
    {
        public void Save(IList<User> users)
        {
            using (var session = GetSession())
            {
                foreach (var user in users)
                {
                    var rawUser = RawUser.Create(user);
                    session.Store(rawUser);
                }

                session.SaveChanges();
            }
        }
    }
}
