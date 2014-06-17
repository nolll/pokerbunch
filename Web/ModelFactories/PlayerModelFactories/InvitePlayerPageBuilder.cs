using Core.Entities;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.PlayerModels.Invite;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class InvitePlayerPageBuilder : IInvitePlayerPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public InvitePlayerPageBuilder(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        private InvitePlayerPageModel Create(Homegame homegame)
        {
            return new InvitePlayerPageModel
                {
                    BrowserTitle = "Invite Player",
                    PageProperties = _pagePropertiesFactory.Create(homegame)
                };
        }

        public InvitePlayerPageModel Build(Homegame homegame, InvitePlayerPostModel postModel)
        {
            var model = Create(homegame);
            if (postModel != null)
            {
                model.Email = postModel.Email;
            }
            return model;
        }
    }
}