using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Tests.Common.Builders;

namespace Tests.Common
{
    public class FakePlayerRepository : IPlayerRepository
    {
        private readonly IList<Player> _list;

        public FakePlayerRepository()
        {
            _list = CreateList();
        }

        public IList<Player> GetList(int bunchId)
        {
            return _list;
        }

        public IList<Player> GetList(IList<int> ids)
        {
            throw new System.NotImplementedException();
        }

        public Player GetById(int id)
        {
            return _list.FirstOrDefault(o => o.Id == id);
        }

        public Player GetByName(int bunchId, string name)
        {
            return _list.FirstOrDefault(o => o.BunchId == bunchId && o.DisplayName == name);
        }

        public Player GetByUserName(int bunchId, string userName)
        {
            throw new System.NotImplementedException();
        }

        public int Add(Player player)
        {
            throw new System.NotImplementedException();
        }

        public bool JoinHomegame(Player player, Bunch bunch, User user)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int playerId)
        {
            throw new System.NotImplementedException();
        }

        private IList<Player> CreateList()
        {
            return new List<Player>
            {
                new PlayerBuilder()
                    .WithId(Constants.PlayerIdA)
                    .Build(),
                new PlayerBuilder()
                    .WithId(Constants.PlayerIdB)
                    .Build()
            };
        }
    }
}