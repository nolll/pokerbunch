using Core.UseCases;
using Core.UseCases.BunchList;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.HomegameModels.List;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class BunchListPageBuilder : IBunchListPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IBunchListItemModelFactory _bunchListItemModelFactory;
        private readonly IBunchListInteractor _bunchListInteractor;

        public BunchListPageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            IBunchListItemModelFactory bunchListItemModelFactory,
            IBunchListInteractor bunchListInteractor)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _bunchListItemModelFactory = bunchListItemModelFactory;
            _bunchListInteractor = bunchListInteractor;
        }

        public BunchListPageModel Build()
        {
            var result = _bunchListInteractor.Execute();

            return new BunchListPageModel
            {
                BrowserTitle = "Bunches",
                PageProperties = _pagePropertiesFactory.Create(),
                BunchModels = _bunchListItemModelFactory.CreateList(result.Bunches)
            };
        }
    }
}