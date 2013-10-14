using System.Collections.Generic;
using Core.Classes;
using Web.ModelFactories.MiscModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.PlayerModels.Achievements;
using Web.Models.PlayerModels.Details;
using Web.Models.PlayerModels.Facts;
using Web.Models.UrlModels;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class PlayerDetailsPageModelFactory : IPlayerDetailsPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IAvatarModelFactory _avatarModelFactory;

        public PlayerDetailsPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            IAvatarModelFactory avatarModelFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _avatarModelFactory = avatarModelFactory;
        }

        public PlayerDetailsPageModel Create(User currentUser, Homegame homegame, Player player, User user, IList<Cashgame> cashgames, bool isManager, bool hasPlayed, Cashgame runningGame = null)
        {
            var hasUser = user != null;

            var model = new PlayerDetailsPageModel
                {
                    BrowserTitle = "Player Details",
                    PageProperties = _pagePropertiesFactory.Create(currentUser, homegame, runningGame),
                    DisplayName = player.DisplayName,
                    DeleteUrl = new PlayerDeleteUrlModel(homegame, player),
                    DeleteEnabled = isManager && !hasPlayed,
                    ShowUserInfo = hasUser,
                    ShowInvitation = !hasUser,
                    PlayerFactsModel = new PlayerFactsModel(homegame, cashgames, player),
                    PlayerBadgesModel = new PlayerBadgesModel(player, cashgames)
                };

            if (hasUser)
            {
                model.UserUrl = new UserDetailsUrlModel(user);
                model.UserEmail = user.Email;
                model.AvatarModel = _avatarModelFactory.Create(user.Email);
            }
            else
            {
                model.InvitationUrl = new PlayerInviteUrlModel(homegame, player);
            }
            return model;
        }
    }
}