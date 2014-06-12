using Application.UseCases.ApplicationContext;
using Application.UseCases.BunchList;
using Web.Models.HomegameModels.List;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class BunchListPageBuilder : IBunchListPageBuilder
    {
        private readonly IApplicationContextInteractor _applicationContextInteractor;
        private readonly IBunchListInteractor _bunchListInteractor;

        public BunchListPageBuilder(
            IApplicationContextInteractor applicationContextInteractor,
            IBunchListInteractor bunchListInteractor)
        {
            _applicationContextInteractor = applicationContextInteractor;
            _bunchListInteractor = bunchListInteractor;
        }

        public BunchListPageModel Build()
        {
            var applicationContextResult = _applicationContextInteractor.Execute();
            var bunchListResult = _bunchListInteractor.Execute();

            return new BunchListPageModel(
                applicationContextResult,
                bunchListResult);
        }
    }
}