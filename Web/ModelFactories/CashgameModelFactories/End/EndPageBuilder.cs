using Application.UseCases.BunchContext;
using Web.Models.CashgameModels.End;
using Web.Models.PageBaseModels;

namespace Web.ModelFactories.CashgameModelFactories.End
{
    public class EndPageBuilder : IEndPageBuilder
    {
        private readonly IBunchContextInteractor _bunchContextInteractor;

        public EndPageBuilder(
            IBunchContextInteractor bunchContextInteractor)
        {
            _bunchContextInteractor = bunchContextInteractor;
        }

        public EndPageModel Build(string slug)
        {
            var contextResult = _bunchContextInteractor.Execute(new BunchContextRequest{Slug = slug});
            
            return new EndPageModel
                {
                    BrowserTitle = "End Game",
                    PageProperties = new PageProperties(contextResult),
                    ShowDiff = true
                };
        }
    }
}