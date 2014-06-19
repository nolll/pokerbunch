using Application.UseCases.BunchContext;
using Web.Models.HomeModels;

namespace Web.ModelFactories.HomeModelFactories
{
    public class HomePageBuilder : IHomePageBuilder
    {
        private readonly IBunchContextInteractor _bunchContextInteractor;

        public HomePageBuilder(IBunchContextInteractor bunchContextInteractor)
        {
            _bunchContextInteractor = bunchContextInteractor;
        }

        public HomePageModel Build()
        {
            var bunchContextRequest = new BunchContextRequest();
            var contextResult = _bunchContextInteractor.Execute(bunchContextRequest);

            return new HomePageModel(contextResult);
        }
    }
}