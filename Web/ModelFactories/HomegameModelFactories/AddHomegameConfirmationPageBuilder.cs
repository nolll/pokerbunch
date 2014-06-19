using Application.UseCases.AppContext;
using Web.Models.HomegameModels.Add;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class AddHomegameConfirmationPageBuilder : IAddHomegameConfirmationPageBuilder
    {
        private readonly IAppContextInteractor _contextInteractor;

        public AddHomegameConfirmationPageBuilder(IAppContextInteractor contextInteractor)
        {
            _contextInteractor = contextInteractor;
        }

        public AddHomegameConfirmationPageModel Build()
        {
            var contextResult = _contextInteractor.Execute();

            return new AddHomegameConfirmationPageModel(contextResult);
        }
    }
}