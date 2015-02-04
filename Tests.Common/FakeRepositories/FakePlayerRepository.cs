using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Tests.Common.Builders;

namespace Tests.Common.FakeRepositories
{
    public class FakePlayerRepository : IPlayerRepository
    {
        public Player Added { get; private set; }
        public int Deleted { get; private set; }
        public JoinedData Joined { get; private set; }
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
            return _list.Where(o => ids.Contains(o.Id)).ToList();
        }

        public Player GetById(int id)
        {
            return _list.FirstOrDefault(o => o.Id == id);
        }

        public Player GetByName(int bunchId, string name)
        {
            return _list.FirstOrDefault(o => o.BunchId == bunchId && o.DisplayName == name);
        }

        public Player GetByUserId(int bunchId, int userId)
        {
            return _list.FirstOrDefault(o => o.UserId == userId);
        }

        public int Add(Player player)
        {
            Added = player;
            return 1;
        }

        public bool JoinHomegame(Player player, Bunch bunch, int userId)
        {
            Joined = new JoinedData(player.Id, bunch.Id, userId);
            return true;
        }

        public bool Delete(int playerId)
        {
            Deleted = playerId;
            return true;
        }

        private IList<Player> CreateList()
        {
            return new List<Player>
            {
                new PlayerBuilder()
                    .WithId(Constants.PlayerIdA)
                    .WithUserId(Constants.UserIdA)
                    .WithDisplayName(Constants.PlayerNameA)
                    .WithRole(Role.Player)
                    .Build(),
                new PlayerBuilder()
                    .WithId(Constants.PlayerIdB)
                    .WithUserId(Constants.UserIdB)
                    .WithDisplayName(Constants.PlayerNameB)
                    .WithRole(Role.Player)
                    .Build(),
                new PlayerBuilder()
                    .WithId(Constants.PlayerIdC)
                    .WithDisplayName(Constants.PlayerNameC)
                    .WithRole(Role.Player)
                    .Build()
            };
        }

        public class JoinedData
        {
            public JoinedData(int playerId, int bunchId, int userId)
            {
                PlayerId = playerId;
                BunchId = bunchId;
                UserId = userId;
            }

            public int PlayerId { get; set; }
            public int BunchId { get; set; }
            public int UserId { get; set; }
        }
    }
}