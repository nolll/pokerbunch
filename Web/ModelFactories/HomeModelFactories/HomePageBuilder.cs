using Application.UseCases.BunchContext;
using Web.Models.HomeModels;

namespace Web.ModelFactories.HomeModelFactories
{
    public class HomePageBuilder : IHomePageBuilder
    {
        private readonly IBunchContextInteractor _contextInteractor;

        public HomePageBuilder(IBunchContextInteractor contextInteractor)
        {
            _contextInteractor = contextInteractor;
        }

        public HomePageModel Build()
        {
            var bunchContextRequest = new BunchContextRequest();
            var contextResult = _contextInteractor.Execute(bunchContextRequest);

            return new HomePageModel(contextResult);
        }
    }
}