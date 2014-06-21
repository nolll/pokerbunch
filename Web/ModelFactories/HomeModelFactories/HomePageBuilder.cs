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
            var contextResult = _contextInteractor.Execute(new BunchContextRequest());

            return new HomePageModel(contextResult);
        }
    }
}