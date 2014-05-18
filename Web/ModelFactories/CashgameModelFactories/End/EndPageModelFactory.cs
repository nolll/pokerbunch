using Core.Entities;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.End;

namespace Web.ModelFactories.CashgameModelFactories.End
{
    public class EndPageModelFactory : IEndPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public EndPageModelFactory(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public EndPageModel Create(Homegame homegame)
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