using Core.Entities;
using Core.Repositories;
using Core.Services;
using Core.Urls;

namespace Core.UseCases
{
    public class PlayerDetails
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IUserRepository _userRepository;

        public PlayerDetails(IBunchRepository bunchRepository,  IPlayerRepository playerRepository, ICashgameRepository cashgameRepository, IUserRepository userRepository)
        {
            _bunchRepository = bunchRepository;
            _playerRepository = playerRepository;
            _cashgameRepository = cashgameRepository;
            _userRepository = userRepository;
        }

        public Result Execute(Request request)
        {
            var player = _playerRepository.GetById(request.PlayerId);
            var bunch = _bunchRepository.GetById(player.BunchId);
            var user = _userRepository.GetById(player.UserId);
            var currentUser = _userRepository.GetByNameOrEmail(request.UserName);
            var currentPlayer = _playerRepository.GetByUserId(bunch.Id, currentUser.Id);
            RoleHandler.RequirePlayer(currentUser, currentPlayer);
            var isManager = RoleHandler.IsInRole(currentUser, currentPlayer, Role.Manager);
            var hasPlayed = _cashgameRepository.HasPlayed(request.PlayerId);
            var avatarUrl = user != null ? GravatarService.GetAvatarUrl(user.Email) : string.Empty;

            return new Result(bunch, player, user, isManager, hasPlayed, avatarUrl);
        }

        public class Request
        {
            public string UserName { get; private set; }
            public int PlayerId { get; private set; }

            public Request(string userName, int playerId)
            {
                UserName = userName;
                PlayerId = playerId;
            }
        }

        public class Result
        {
            public string DisplayName { get; private set; }
            public DeletePlayerUrl DeleteUrl { get; private set; }
            public bool CanDelete { get; private set; }
            public bool IsUser { get; private set; }
            public Url UserUrl { get; private set; }
            public string AvatarUrl { get; private set; }
            public Url InvitationUrl { get; private set; }

            public Result(Bunch bunch, Player player, User user, bool isManager, bool hasPlayed, string avatarUrl)
            {
                var isUser = user != null;

                DisplayName = player.DisplayName;
                DeleteUrl = new DeletePlayerUrl(bunch.Slug, player.Id);
                CanDelete = isManager && !hasPlayed;
                IsUser = isUser;
                UserUrl = isUser ? new UserDetailsUrl(user.UserName) : Url.Empty;
                AvatarUrl = avatarUrl;
                InvitationUrl = new InvitePlayerUrl(bunch.Slug, player.Id);
            }
        }
    }
}