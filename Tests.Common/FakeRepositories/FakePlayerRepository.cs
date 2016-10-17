using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;

namespace Tests.Common.FakeRepositories
{
    public class FakePlayerRepository : IPlayerRepository
    {
        public Player Added { get; private set; }
        public string Deleted { get; private set; }
        public JoinedData Joined { get; private set; }
        private readonly IList<Player> _list;

        public FakePlayerRepository()
        {
            _list = CreateList();
        }

        public IList<string> Find(string bunchId)
        {
            return _list.Where(o => o.BunchId == bunchId).Select(o => o.Id).ToList();
        }

        public IList<string> FindByName(string bunchId, string name)
        {
            return _list.Where(o => o.BunchId == bunchId && o.DisplayName == name).Select(o => o.Id).ToList();
        }

        public IList<string> FindByUserId(string bunchId, string userId)
        {
            return _list.Where(o => o.BunchId == bunchId && o.UserId == userId).Select(o => o.Id).ToList();
        }

        public IList<Player> Get(IList<string> ids)
        {
            return _list.Where(o => ids.Contains(o.Id)).ToList();
        }

        public Player Get(string id)
        {
            return _list.FirstOrDefault(o => o.Id == id);
        }

        public string Add(Player player)
        {
            Added = player;
            return "1";
        }

        public bool JoinHomegame(Player player, Bunch bunch, string userId)
        {
            Joined = new JoinedData(player.Id, bunch.Id, userId);
            return true;
        }

        public void Delete(string playerId)
        {
            Deleted = playerId;
        }

        private IList<Player> CreateList()
        {
            return new List<Player>
            {
                TestData.PlayerA,
                TestData.PlayerB,
                TestData.PlayerC,
                TestData.PlayerD
            };
        }

        public class JoinedData
        {
            public string PlayerId { get; private set; }
            public string BunchId { get; private set; }
            public string UserId { get; private set; }

            public JoinedData(string playerId, string bunchId, string userId)
            {
                PlayerId = playerId;
                BunchId = bunchId;
                UserId = userId;
            }
        }
    }
}