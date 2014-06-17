using Core.Entities;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.PlayerModels.Invite;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class InvitePlayerConfirmationPageBuilder : IInvitePlayerConfirmationPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public InvitePlayerConfirmationPageBuilder(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public InvitePlayerConfirmationPageModel Build(Homegame homegame)
        {
            return new InvitePlayerConfirmationPageModel
                {
                    BrowserTitle = "Player Invited",
                    PageProperties = _pagePropertiesFactory.Create(homegame),
                };
        }
    }
}