using Application.Services;
using Core.Entities;
using Core.Repositories;

namespace Application.UseCases.PlayerDetails
{
    public class PlayerDetailsInteractor : IPlayerDetailsInteractor
    {
        private readonly IAuth _auth;
        private readonly IBunchRepository _bunchRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IUserRepository _userRepository;

        public PlayerDetailsInteractor(
            IAuth auth,
            IBunchRepository bunchRepository,
            IPlayerRepository playerRepository,
            ICashgameRepository cashgameRepository,
            IUserRepository userRepository)
        {
            _auth = auth;
            _bunchRepository = bunchRepository;
            _playerRepository = playerRepository;
            _cashgameRepository = cashgameRepository;
            _userRepository = userRepository;
        }

        public PlayerDetailsResult Execute(PlayerDetailsRequest request)
        {
            var homegame = _bunchRepository.GetBySlug(request.Slug);
            var player = _playerRepository.GetById(request.PlayerId);
            var user = _userRepository.GetById(player.UserId);
            var isManager = _auth.IsInRole(request.Slug, Role.Manager);
            var hasPlayed = _cashgameRepository.HasPlayed(request.PlayerId);
            var avatarUrl = user != null ? GravatarService.GetAvatarUrl(user.Email) : string.Empty;
            
            return new PlayerDetailsResult(homegame, player, user, isManager, hasPlayed, avatarUrl);
        }
    }
}