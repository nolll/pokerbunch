using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Web.Models.PlayerModels.Badges;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class PlayerBadgesModelFactory : IPlayerBadgesModelFactory
    {
        private readonly IBadgeModelFactory _badgeModelFactory;

        public PlayerBadgesModelFactory(IBadgeModelFactory badgeModelFactory)
        {
            _badgeModelFactory = badgeModelFactory;
        }

        public PlayerBadgesModel Create(int playerId, IList<Cashgame> cashgames)
        {
            var gameCount = GetNumberOfPlayedGames(playerId, cashgames);

            return new PlayerBadgesModel(
                    _badgeModelFactory.Create("Played one game", PlayedEnoughGames(gameCount, 1)),
                    _badgeModelFactory.Create("Played ten games", PlayedEnoughGames(gameCount, 10)),
                    _badgeModelFactory.Create("Played 50 games", PlayedEnoughGames(gameCount, 50)),
                    _badgeModelFactory.Create("Played 100 games", PlayedEnoughGames(gameCount, 100)),
                    _badgeModelFactory.Create("Played 200 games", PlayedEnoughGames(gameCount, 200)),
                    _badgeModelFactory.Create("Played 500 games", PlayedEnoughGames(gameCount, 500)));
        }

        private int GetNumberOfPlayedGames(int playerId, IList<Cashgame> cashgames)
        {
            if (cashgames == null)
            {
                return 0;
            }
            return cashgames.Count(cashgame => cashgame.IsInGame(playerId));
        }

        private bool PlayedEnoughGames(int gameCount, int requiredGameCount)
        {
            return gameCount >= requiredGameCount;
        }
    }
}