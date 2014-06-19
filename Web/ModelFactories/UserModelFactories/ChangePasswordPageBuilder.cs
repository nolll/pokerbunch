using Application.UseCases.AppContext;
using Web.Models.PageBaseModels;
using Web.Models.UserModels.ChangePassword;

namespace Web.ModelFactories.UserModelFactories
{
    public class ChangePasswordPageBuilder : IChangePasswordPageBuilder
    {
        private readonly IAppContextInteractor _appContextInteractor;

        public ChangePasswordPageBuilder(IAppContextInteractor appContextInteractor)
        {
            _appContextInteractor = appContextInteractor;
        }

        public ChangePasswordPageModel Build()
        {
            var contextResult = _appContextInteractor.Execute();

            return new ChangePasswordPageModel
                {
                    BrowserTitle = "Change Password",
                    PageProperties = new PageProperties(contextResult)
                };
        }

        public ChangePasswordConfirmationPageModel BuildConfirmation()
        {
            var contextResult = _appContextInteractor.Execute();

            return new ChangePasswordConfirmationPageModel
                {
                    BrowserTitle = "Password Changed",
                    PageProperties = new PageProperties(contextResult)
                };
        }
    }
}