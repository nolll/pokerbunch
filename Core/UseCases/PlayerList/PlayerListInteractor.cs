using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases.PlayerList
{
    public class PlayerListInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IAuth _auth;

        public PlayerListInteractor(IBunchRepository bunchRepository, IPlayerRepository playerRepository, IAuth auth)
        {
            _bunchRepository = bunchRepository;
            _playerRepository = playerRepository;
            _auth = auth;
        }

        public PlayerListResult Execute(PlayerListRequest request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var players = _playerRepository.GetList(bunch.Id);
            var isManager = _auth.IsInRole(request.Slug, Role.Manager);

            return new PlayerListResult(bunch, players, isManager);
        }
    }
}