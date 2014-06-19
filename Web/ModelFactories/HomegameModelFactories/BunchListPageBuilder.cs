using Application.UseCases.AppContext;
using Application.UseCases.BunchList;
using Web.Models.HomegameModels.List;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class BunchListPageBuilder : IBunchListPageBuilder
    {
        private readonly IAppContextInteractor _contextInteractor;
        private readonly IBunchListInteractor _bunchListInteractor;

        public BunchListPageBuilder(
            IAppContextInteractor contextInteractor,
            IBunchListInteractor bunchListInteractor)
        {
            _contextInteractor = contextInteractor;
            _bunchListInteractor = bunchListInteractor;
        }

        public BunchListPageModel Build()
        {
            var contextResult = _contextInteractor.Execute();
            var bunchListResult = _bunchListInteractor.Execute();

            return new BunchListPageModel(
                contextResult,
                bunchListResult);
        }
    }
}