using Core.Entities;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.End;

namespace Web.ModelFactories.CashgameModelFactories.End
{
    public class EndPageBuilder : IEndPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public EndPageBuilder(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public EndPageModel Build(Homegame homegame)
        {
            return new EndPageModel
                {
                    BrowserTitle = "End Game",
                    PageProperties = _pagePropertiesFactory.Create(homegame),
                    ShowDiff = true
                };
        }

    }
}