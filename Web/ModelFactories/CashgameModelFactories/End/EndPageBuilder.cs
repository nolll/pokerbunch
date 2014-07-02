using Application.UseCases.BunchContext;
using Web.Models.CashgameModels.End;

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

        public EndGamePageModel Build(string slug)
        {
            var contextResult = _contextInteractor.Execute(new BunchContextRequest(slug));
            
            return new EndGamePageModel(contextResult);
        }
    }
}