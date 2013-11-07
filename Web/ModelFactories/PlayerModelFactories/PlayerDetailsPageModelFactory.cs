using System.Collections.Generic;
using Core.Classes;
using Core.Services;
using Web.ModelFactories.MiscModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.PlayerModels.Achievements;
using Web.Models.PlayerModels.Details;
using Web.Models.PlayerModels.Facts;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class PlayerDetailsPageModelFactory : IPlayerDetailsPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IAvatarModelFactory _avatarModelFactory;
        private readonly IUrlProvider _urlProvider;
        private readonly IPlayerFactsModelFactory _playerFactsModelFactory;

        public PlayerDetailsPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            IAvatarModelFactory avatarModelFactory,
            IUrlProvider urlProvider,
            IPlayerFactsModelFactory playerFactsModelFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _avatarModelFactory = avatarModelFactory;
            _urlProvider = urlProvider;
            _playerFactsModelFactory = playerFactsModelFactory;
        }

        public PlayerDetailsPageModel Create(User currentUser, Homegame homegame, Player player, User user, IList<Cashgame> cashgames, bool isManager, bool hasPlayed, Cashgame runningGame = null)
        {
            var hasUser = user != null;

            var model = new PlayerDetailsPageModel
                {
                    BrowserTitle = "Player Details",
                    PageProperties = _pagePropertiesFactory.Create(currentUser, homegame, runningGame),
                    DisplayName = player.DisplayName,
                    DeleteUrl = _urlProvider.GetPlayerDeleteUrl(homegame, player),
                    DeleteEnabled = isManager && !hasPlayed,
                    ShowUserInfo = hasUser,
                    ShowInvitation = !hasUser,
                    PlayerFactsModel = _playerFactsModelFactory.Create(homegame.Currency, cashgames, player),
                    PlayerBadgesModel = new PlayerBadgesModel(player, cashgames)
                };

            if (hasUser)
            {
                model.UserUrl = _urlProvider.GetUserDetailsUrl(user);
                model.UserEmail = user.Email;
                model.AvatarModel = _avatarModelFactory.Create(user.Email);
            }
            else
            {
                model.InvitationUrl = _urlProvider.GetPlayerInviteUrl(homegame, player);
            }
            return model;
        }
    }
}