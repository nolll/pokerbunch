using System.Linq;
using Core.Classes;
using Core.Repositories;
using Tests.Core.UseCases;

namespace Core.UseCases.ShowPlayerList
{
    public class ShowPlayerListInteractor : IShowPlayerListInteractor
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
                    Slug = slug,
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