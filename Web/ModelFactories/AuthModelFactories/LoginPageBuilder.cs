using Application.Services;
using Application.Urls;
using Application.UseCases.ApplicationContext;
using Web.Models.AuthModels;
using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.ModelFactories.AuthModelFactories
{
    public class LoginPageBuilder : ILoginPageBuilder
    {
        private readonly IWebContext _webContext;
        private readonly IApplicationContextInteractor _applicationContextInteractor;

        public LoginPageBuilder(
            IWebContext webContext,
            IApplicationContextInteractor applicationContextInteractor)
        {
            _webContext = webContext;
            _applicationContextInteractor = applicationContextInteractor;
        }

        private LoginPageModel Create()
        {
            var returnUrl = _webContext.GetQueryParam("return");
            var returnUrlModel = returnUrl != null ? new Url(returnUrl) : new HomeUrl();
            var applicationContextResult = _applicationContextInteractor.Execute();

            return new LoginPageModel
                {
                    BrowserTitle = "Login",
                    PageProperties = new PageProperties(applicationContextResult),
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