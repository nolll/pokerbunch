using Application.Services;
using Core.Entities;
using Core.Repositories;

namespace Application.UseCases.PlayerList
{
    public class PlayerListInteractor : IPlayerListInteractor
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IAuth _auth;

        public PlayerListInteractor(
            IHomegameRepository homegameRepository,
            IPlayerRepository playerRepository,
            IAuth auth)
        {
            _homegameRepository = homegameRepository;
            _playerRepository = playerRepository;
            _auth = auth;
        }

        public PlayerListResult Execute(PlayerListRequest request)
        {
            var homegame = _homegameRepository.GetBySlug(request.Slug);
            var players = _playerRepository.GetList(homegame);
            var isManager = _auth.IsInRole(request.Slug, Role.Manager);

            return new PlayerListResult(homegame, players, isManager);
        }
    }
}