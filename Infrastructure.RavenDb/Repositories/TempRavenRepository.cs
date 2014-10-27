using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Infrastructure.Storage;

namespace Infrastructure.RavenDb.Repositories
{
    public class TempRavenRepository : RavenRepository, IRavenUserRepository
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

        public void Save(IList<Player> players)
        {
            using (var session = GetSession())
            {
                foreach (var player in players)
                {
                    var rawPlayer = RawPlayer.Create(player);
                    session.Store(rawPlayer);
                }

                session.SaveChanges();
            }
        }

        public void Save(IList<Checkpoint> checkpoints)
        {
            using (var session = GetSession())
            {
                foreach (var checkpoint in checkpoints)
                {
                    var rawCheckpoint = RawCheckpoint.Create(checkpoint);
                    session.Store(rawCheckpoint);
                }

                session.SaveChanges();
            }
        }

        public void Save(IList<Bunch> bunches)
        {
            using (var session = GetSession())
            {
                foreach (var bunch in bunches)
                {
                    var rawBunch = RawBunch.Create(bunch);
                    session.Store(rawBunch);
                }

                session.SaveChanges();
            }
        }
    }
}
