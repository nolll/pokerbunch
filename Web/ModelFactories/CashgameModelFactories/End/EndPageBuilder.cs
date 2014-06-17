using Core.Repositories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.End;

namespace Web.ModelFactories.CashgameModelFactories.End
{
    public class EndPageBuilder : IEndPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IHomegameRepository _homegameRepository;

        public EndPageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            IHomegameRepository homegameRepository)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _homegameRepository = homegameRepository;
        }

        public EndPageModel Build(string slug)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            
            return new EndPageModel
                {
                    BrowserTitle = "End Game",
                    PageProperties = _pagePropertiesFactory.Create(homegame),
                    ShowDiff = true
                };
        }
    }
}