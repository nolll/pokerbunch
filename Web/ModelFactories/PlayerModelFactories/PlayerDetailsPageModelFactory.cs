using System.Collections.Generic;
using Application.Services.Interfaces;
using Core.Classes;
using Web.ModelFactories.MiscModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.PlayerModels.Achievements;
using Web.Models.PlayerModels.Details;

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

        public PlayerDetailsPageModel Create(User currentUser, Homegame homegame, Player player, User user, IList<Cashgame> cashgames, bool isManager, bool hasPlayed)
        {
            var hasUser = user != null;

            var model = new PlayerDetailsPageModel
                {
                    BrowserTitle = "Player Details",
                    PageProperties = _pagePropertiesFactory.Create(currentUser, homegame),
                    DisplayName = player.DisplayName,
                    DeleteUrl = _urlProvider.GetPlayerDeleteUrl(homegame.Slug, player.DisplayName),
                    DeleteEnabled = isManager && !hasPlayed,
                    ShowUserInfo = hasUser,
                    ShowInvitation = !hasUser,
                    PlayerFactsModel = _playerFactsModelFactory.Create(homegame.Currency, cashgames, player),
                    PlayerBadgesModel = new PlayerBadgesModel(player.Id, cashgames)
                };

            if (hasUser)
            {
                model.UserUrl = _urlProvider.GetUserDetailsUrl(user.UserName);
                model.UserEmail = user.Email;
                model.AvatarModel = _avatarModelFactory.Create(user.Email);
            }
            else
            {
                model.InvitationUrl = _urlProvider.GetPlayerInviteUrl(homegame.Slug, player.DisplayName);
            }
            return model;
        }
    }
}