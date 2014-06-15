using System.Collections.Generic;
using Application.Urls;
using Core.Entities;
using Web.ModelFactories.MiscModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.PlayerModels.Details;
using Web.Models.UrlModels;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class PlayerDetailsPageModelFactory : IPlayerDetailsPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IAvatarModelFactory _avatarModelFactory;
        private readonly IPlayerFactsModelFactory _playerFactsModelFactory;
        private readonly IPlayerBadgesModelFactory _playerBadgesModelFactory;

        public PlayerDetailsPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            IAvatarModelFactory avatarModelFactory,
            IPlayerFactsModelFactory playerFactsModelFactory,
            IPlayerBadgesModelFactory playerBadgesModelFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _avatarModelFactory = avatarModelFactory;
            _playerFactsModelFactory = playerFactsModelFactory;
            _playerBadgesModelFactory = playerBadgesModelFactory;
        }

        public PlayerDetailsPageModel Create(Homegame homegame, Player player, User user, IList<Cashgame> cashgames, bool isManager, bool hasPlayed)
        {
            var hasUser = user != null;
            var userUrl = hasUser ? new UserDetailsUrl(user.UserName) : null;
            var userEmail = hasUser ? user.Email : null;
            var avatarModel = hasUser ? _avatarModelFactory.Create(user.Email) : null;
            var invitationUrl = new InvitePlayerUrl(homegame.Slug, player.Id);

            return new PlayerDetailsPageModel
                {
                    BrowserTitle = "Player Details",
                    PageProperties = _pagePropertiesFactory.Create(homegame),
                    DisplayName = player.DisplayName,
                    DeleteUrl = new DeletePlayerUrl(homegame.Slug, player.Id),
                    DeleteEnabled = isManager && !hasPlayed,
                    ShowUserInfo = hasUser,
                    PlayerFactsModel = _playerFactsModelFactory.Create(homegame.Currency, cashgames, player),
                    PlayerBadgesModel = _playerBadgesModelFactory.Create(player.Id, cashgames),
                    UserUrl = userUrl,
                    UserEmail = userEmail,
                    AvatarModel = avatarModel,
                    InvitationUrl = invitationUrl
                };
        }
    }
}