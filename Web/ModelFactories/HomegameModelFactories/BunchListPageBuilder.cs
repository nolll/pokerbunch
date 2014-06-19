using Application.UseCases.ApplicationContext;
using Application.UseCases.BunchList;
using Web.Models.HomegameModels.List;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class BunchListPageBuilder : IBunchListPageBuilder
    {
        private readonly IAppContextInteractor _appContextInteractor;
        private readonly IBunchListInteractor _bunchListInteractor;

        public BunchListPageBuilder(
            IAppContextInteractor appContextInteractor,
            IBunchListInteractor bunchListInteractor)
        {
            _appContextInteractor = appContextInteractor;
            _bunchListInteractor = bunchListInteractor;
        }

        public BunchListPageModel Build()
        {
            var applicationContextResult = _appContextInteractor.Execute();
            var bunchListResult = _bunchListInteractor.Execute();

            return new BunchListPageModel(
                applicationContextResult,
                bunchListResult);
        }
    }
}