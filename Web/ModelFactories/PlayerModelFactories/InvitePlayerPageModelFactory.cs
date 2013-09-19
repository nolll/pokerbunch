using Core.Classes;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.PlayerModels.Invite;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class InvitePlayerPageModelFactory : IInvitePlayerPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public InvitePlayerPageModelFactory(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public InvitePlayerPageModel Create(User user, Homegame homegame, Cashgame runningGame)
        {
            return new InvitePlayerPageModel
                {
                    BrowserTitle = "Invite Player",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame, runningGame)
                };
        }

        public InvitePlayerPageModel Create(User user, Homegame homegame, Cashgame runningGame, InvitePlayerPostModel postModel)
        {
            var model = Create(user, homegame, runningGame);
            model.Email = postModel.Email;
            return model;
        }
    }
}