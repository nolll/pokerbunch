using System.Linq;
using Core.Classes;
using Core.Repositories;

namespace Application.UseCases.PlayerList
{
    public class PlayerListInteractor : IPlayerListInteractor
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly IPlayerRepository _playerRepository;

        public PlayerListInteractor(
            IHomegameRepository homegameRepository,
            IPlayerRepository playerRepository)
        {
            _homegameRepository = homegameRepository;
            _playerRepository = playerRepository;
        }

        public PlayerListResult Execute(PlayerListRequest request)
        {
            var homegame = _homegameRepository.GetBySlug(request.Slug);
            var players = _playerRepository.GetList(homegame);

            return new PlayerListResult
                {
                    Slug = request.Slug,
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