using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases.PlayerDetails
{
    public static class PlayerDetailsInteractor
    {
        public static PlayerDetailsResult Execute(
            IAuth auth,
            IBunchRepository bunchRepository,
            IPlayerRepository playerRepository,
            ICashgameRepository cashgameRepository,
            IUserRepository userRepository,
            PlayerDetailsRequest request)
        {
            var homegame = bunchRepository.GetBySlug(request.Slug);
            var player = playerRepository.GetById(request.PlayerId);
            var user = userRepository.GetById(player.UserId);
            var isManager = auth.IsInRole(request.Slug, Role.Manager);
            var hasPlayed = cashgameRepository.HasPlayed(request.PlayerId);
            var avatarUrl = user != null ? GravatarService.GetAvatarUrl(user.Email) : string.Empty;
            
            return new PlayerDetailsResult(homegame, player, user, isManager, hasPlayed, avatarUrl);
        }
    }
}