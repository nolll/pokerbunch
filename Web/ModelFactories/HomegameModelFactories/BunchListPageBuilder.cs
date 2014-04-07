using Core.UseCases;
using Core.UseCases.ShowBunchList;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.HomegameModels.List;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class BunchListPageBuilder : IBunchListPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IBunchListItemModelFactory _bunchListItemModelFactory;
        private readonly IShowBunchListInteractor _showBunchListInteractor;

        public BunchListPageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            IBunchListItemModelFactory bunchListItemModelFactory,
            IShowBunchListInteractor showBunchListInteractor)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _bunchListItemModelFactory = bunchListItemModelFactory;
            _showBunchListInteractor = showBunchListInteractor;
        }

        public BunchListPageModel Build()
        {
            var result = _showBunchListInteractor.Execute();

            return new BunchListPageModel
            {
                BrowserTitle = "Bunches",
                PageProperties = _pagePropertiesFactory.Create(),
                BunchModels = _bunchListItemModelFactory.CreateList(result.Bunches)
            };
        }
    }
}