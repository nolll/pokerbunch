using Application.Services;
using Core.Entities;
using Core.Repositories;

namespace Application.UseCases.PlayerList
{
    public class PlayerListInteractor : IPlayerListInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IAuth _auth;

        public PlayerListInteractor(
            IBunchRepository bunchRepository,
            IPlayerRepository playerRepository,
            IAuth auth)
        {
            _bunchRepository = bunchRepository;
            _playerRepository = playerRepository;
            _auth = auth;
        }

        public PlayerListResult Execute(PlayerListRequest request)
        {
            var homegame = _bunchRepository.GetBySlug(request.Slug);
            var players = _playerRepository.GetList(homegame);
            var isManager = _auth.IsInRole(request.Slug, Role.Manager);

            return new PlayerListResult(homegame, players, isManager);
        }
    }
}