using Core.Classes;
using Web.ModelFactories.HomegameModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.PlayerModels.Add;
using Web.Models.PlayerModels.Invite;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class InvitePlayerConfirmationPageModelFactory : IInvitePlayerConfirmationPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public InvitePlayerConfirmationPageModelFactory(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public InvitePlayerConfirmationPageModel Create(User user, Homegame homegame, Cashgame runningGame)
        {
            return new InvitePlayerConfirmationPageModel
                {
                    BrowserTitle = "Player Added",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame, runningGame),
                };
        }
    }
}