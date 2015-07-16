using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases.PlayerList
{
    public class PlayerListInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPlayerRepository _playerRepository;

        public PlayerListInteractor(IBunchRepository bunchRepository, IUserRepository userRepository, IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _userRepository = userRepository;
            _playerRepository = playerRepository;
        }

        public PlayerListResult Execute(PlayerListRequest request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var player = _playerRepository.GetByUserId(bunch.Id, user.Id);
            var players = _playerRepository.GetList(bunch.Id);
            var isManager = RoleHandler.IsInRole(user, player, Role.Manager);

            return new PlayerListResult(bunch, players, isManager);
        }
    }
}