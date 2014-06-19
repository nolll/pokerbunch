using Application.Services;
using Application.Urls;
using Application.UseCases.AppContext;
using Web.Models.AuthModels;
using Web.Models.PageBaseModels;

namespace Web.ModelFactories.AuthModelFactories
{
    public class LoginPageBuilder : ILoginPageBuilder
    {
        private readonly IWebContext _webContext;
        private readonly IAppContextInteractor _appContextInteractor;

        public LoginPageBuilder(
            IWebContext webContext,
            IAppContextInteractor appContextInteractor)
        {
            _webContext = webContext;
            _appContextInteractor = appContextInteractor;
        }

        private LoginPageModel Create()
        {
            var returnUrl = _webContext.GetQueryParam("return");
            var returnUrlModel = returnUrl != null ? new Url(returnUrl) : new HomeUrl();
            var appContextResult = _appContextInteractor.Execute();

            return new LoginPageModel
                {
                    BrowserTitle = "Login",
                    PageProperties = new PageProperties(appContextResult),
                    ReturnUrl = returnUrlModel.Relative,
                    AddUserUrl = new AddUserUrl(),
                    ForgotPasswordUrl = new ForgotPasswordUrl()
                };
        }

        public LoginPageModel Build(LoginPostModel postModel)
        {
            var model = Create();
            if (postModel != null)
            {
                model.LoginName = postModel.LoginName;
                model.RememberMe = postModel.RememberMe;
                model.ReturnUrl = postModel.ReturnUrl;
            }
            return model;
        }
    }
}