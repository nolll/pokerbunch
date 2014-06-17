using Core.Repositories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.PlayerModels.Invite;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class InvitePlayerConfirmationPageBuilder : IInvitePlayerConfirmationPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IHomegameRepository _homegameRepository;

        public InvitePlayerConfirmationPageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            IHomegameRepository homegameRepository)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _homegameRepository = homegameRepository;
        }

        public InvitePlayerConfirmationPageModel Build(string slug)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            
            return new InvitePlayerConfirmationPageModel
                {
                    BrowserTitle = "Player Invited",
                    PageProperties = _pagePropertiesFactory.Create(homegame),
                };
        }
    }
}