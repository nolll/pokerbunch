using Application.UseCases.BunchContext;
using Web.Models.PlayerModels.Add;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class AddPlayerConfirmationPageBuilder : IAddPlayerConfirmationPageBuilder
    {
        private readonly IBunchContextInteractor _contextInteractor;

        public AddPlayerConfirmationPageBuilder(
            IBunchContextInteractor contextInteractor)
        {
            _contextInteractor = contextInteractor;
        }

        public AddPlayerConfirmationPageModel Build(string slug)
        {
            var contextResult = _contextInteractor.Execute(new BunchContextRequest(slug));

            return new AddPlayerConfirmationPageModel(contextResult);
        }
    }
}