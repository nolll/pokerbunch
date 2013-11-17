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

        public InvitePlayerPageModel Create(User user, Homegame homegame)
        {
            return new InvitePlayerPageModel
                {
                    BrowserTitle = "Invite Player",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame)
                };
        }

        public InvitePlayerPageModel Create(User user, Homegame homegame, InvitePlayerPostModel postModel)
        {
            var model = Create(user, homegame);
            model.Email = postModel.Email;
            return model;
        }
    }
}