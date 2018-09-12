using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;

namespace Infrastructure.Api.FakeServices
{
    public class FakePlayerService : IPlayerService
    {
        public Player Get(string id)
        {
            throw new System.NotImplementedException();
        }

        public IList<Player> List(string bunchId)
        {
            return FakeData.Players.Where(o => o.BunchId == bunchId).ToList();
        }

        public string Add(Player player)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(string playerId)
        {
            throw new System.NotImplementedException();
        }

        public void Invite(string playerId, string email)
        {
            throw new System.NotImplementedException();
        }
    }
}