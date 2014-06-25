using Application.Urls;
using Core.Entities;

namespace Application.UseCases.PlayerDetails
{
    public class PlayerDetailsResult
    {
        public string DisplayName { get; private set; }
        public DeletePlayerUrl DeleteUrl { get; private set; }
        public bool CanDelete { get; private set; }
        public bool IsUser { get; private set; }
        public Url UserUrl { get; private set; }
        public string AvatarUrl { get; private set; }
        public Url InvitationUrl { get; private set; }

        public PlayerDetailsResult(Homegame homegame, Player player, User user, bool isManager, bool hasPlayed, string avatarUrl)
        {
            var isUser = user != null;

            DisplayName = player.DisplayName;
            DeleteUrl = new DeletePlayerUrl(homegame.Slug, player.Id);
            CanDelete = isManager && !hasPlayed;
            IsUser = isUser;
            UserUrl = isUser ? new UserDetailsUrl(user.UserName) : Url.Empty;
            AvatarUrl = avatarUrl;
            InvitationUrl = new InvitePlayerUrl(homegame.Slug, player.Id);
        }
    }
}