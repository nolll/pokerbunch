using Core.UseCases;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.HomegameModels.List;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class BunchListPageBuilder : IBunchListPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IBunchListItemModelFactory _bunchListItemModelFactory;
        private readonly IShowBunchList _showBunchList;

        public BunchListPageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            IBunchListItemModelFactory bunchListItemModelFactory,
            IShowBunchList showBunchList)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _bunchListItemModelFactory = bunchListItemModelFactory;
            _showBunchList = showBunchList;
        }

        public BunchListPageModel Build()
        {
            var result = _showBunchList.Execute();

            return new BunchListPageModel
            {
                BrowserTitle = "Bunches",
                PageProperties = _pagePropertiesFactory.Create(),
                BunchModels = _bunchListItemModelFactory.CreateList(result.Bunches)
            };
        }
    }
}