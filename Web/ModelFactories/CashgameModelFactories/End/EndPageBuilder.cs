using Application.UseCases.BunchContext;
using Web.Models.CashgameModels.End;
using Web.Models.PageBaseModels;

namespace Web.ModelFactories.CashgameModelFactories.End
{
    public class EndPageBuilder : IEndPageBuilder
    {
        private readonly IBunchContextInteractor _contextInteractor;

        public EndPageBuilder(
            IBunchContextInteractor contextInteractor)
        {
            _contextInteractor = contextInteractor;
        }

        public EndPageModel Build(string slug)
        {
            var contextResult = _contextInteractor.Execute(new BunchContextRequest(slug));
            
            return new EndPageModel
                {
                    BrowserTitle = "End Game",
                    PageProperties = new PageProperties(contextResult),
                    ShowDiff = true
                };
        }
    }
}