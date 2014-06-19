using Application.UseCases.AppContext;
using Web.Models.HomegameModels.Add;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class AddHomegameConfirmationPageBuilder : IAddHomegameConfirmationPageBuilder
    {
        private readonly IAppContextInteractor _appContextInteractor;

        public AddHomegameConfirmationPageBuilder(IAppContextInteractor appContextInteractor)
        {
            _appContextInteractor = appContextInteractor;
        }

        public AddHomegameConfirmationPageModel Build()
        {
            var contextResult = _appContextInteractor.Execute();

            return new AddHomegameConfirmationPageModel(contextResult);
        }
    }
}