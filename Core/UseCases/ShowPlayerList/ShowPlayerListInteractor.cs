using System.Linq;
using Core.Classes;
using Core.Repositories;

namespace Tests.Core.UseCases
{
    public class ShowPlayerListInteractor
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly IPlayerRepository _playerRepository;

        public ShowPlayerListInteractor(
            IHomegameRepository homegameRepository,
            IPlayerRepository playerRepository)
        {
            _homegameRepository = homegameRepository;
            _playerRepository = playerRepository;
        }

        public ShowPlayerListResult Execute(string slug)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var players = _playerRepository.GetList(homegame);

            return new ShowPlayerListResult
                {
                    Players = players.Select(CreatePlayerListItem).ToList()
                };
        }

        private PlayerListItem CreatePlayerListItem(Player player)
        {
            return new PlayerListItem
                {
                    Name = player.DisplayName
                };
        } 
    }
}