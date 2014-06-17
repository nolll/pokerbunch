using Core.Entities;
using Core.Repositories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.PlayerModels.Invite;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class InvitePlayerPageBuilder : IInvitePlayerPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IHomegameRepository _homegameRepository;

        public InvitePlayerPageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            IHomegameRepository homegameRepository)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _homegameRepository = homegameRepository;
        }

        private InvitePlayerPageModel Create(Homegame homegame)
        {
            return new InvitePlayerPageModel
                {
                    BrowserTitle = "Invite Player",
                    PageProperties = _pagePropertiesFactory.Create(homegame)
                };
        }

        public InvitePlayerPageModel Build(string slug, InvitePlayerPostModel postModel)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            
            var model = Create(homegame);
            if (postModel != null)
            {
                model.Email = postModel.Email;
            }
            return model;
        }
    }
}