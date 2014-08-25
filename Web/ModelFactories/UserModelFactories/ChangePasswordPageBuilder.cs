using Application.UseCases.AppContext;
using Web.Models.UserModels.ChangePassword;

namespace Web.ModelFactories.UserModelFactories
{
    public class ChangePasswordPageBuilder : IChangePasswordPageBuilder
    {
        private readonly IAppContextInteractor _contextInteractor;

        public ChangePasswordPageBuilder(IAppContextInteractor contextInteractor)
        {
            _contextInteractor = contextInteractor;
        }

        public ChangePasswordPageModel Build()
        {
            var contextResult = _contextInteractor.Execute();

            return new ChangePasswordPageModel(contextResult);
        }

        public ChangePasswordConfirmationPageModel BuildConfirmation()
        {
            var contextResult = _contextInteractor.Execute();

            return new ChangePasswordConfirmationPageModel(contextResult);
        }
    }
}