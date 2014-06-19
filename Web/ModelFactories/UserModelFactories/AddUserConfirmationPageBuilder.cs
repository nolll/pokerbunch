using Application.UseCases.AppContext;
using Web.Models.UserModels.Add;

namespace Web.ModelFactories.UserModelFactories
{
    public class AddUserConfirmationPageBuilder : IAddUserConfirmationPageBuilder
    {
        private readonly IAppContextInteractor _appContextInteractor;

        public AddUserConfirmationPageBuilder(IAppContextInteractor appContextInteractor)
        {
            _appContextInteractor = appContextInteractor;
        }

        public AddUserConfirmationPageModel Build()
        {
            var contextResult = _appContextInteractor.Execute();

            return new AddUserConfirmationPageModel(contextResult);
        }
    }
}