using Application.Services;
using Application.UseCases.ApplicationContext;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.AuthModels;
using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.ModelFactories.AuthModelFactories
{
    public class AuthLoginPageModelFactory : IAuthLoginPageModelFactory
    {
        private readonly IWebContext _webContext;
        private readonly IUrlProvider _urlProvider;
        private readonly IApplicationContextInteractor _applicationContextInteractor;

        public AuthLoginPageModelFactory(
            IWebContext webContext,
            IUrlProvider urlProvider,
            IApplicationContextInteractor applicationContextInteractor)
        {
            _webContext = webContext;
            _urlProvider = urlProvider;
            _applicationContextInteractor = applicationContextInteractor;
        }

        private AuthLoginPageModel Create()
        {
            var returnUrl = _webContext.GetQueryParam("return");
            var returnUrlModel = returnUrl != null ? new UrlModel(returnUrl) : new HomeUrlModel();
            var applicationContextResult = _applicationContextInteractor.Execute();

            return new AuthLoginPageModel
                {
                    BrowserTitle = "Login",
                    PageProperties = new PageProperties(applicationContextResult),
                    ReturnUrl = returnUrlModel.Relative,
                    AddUserUrl = new AddUserUrlModel(),
                    ForgotPasswordUrl = _urlProvider.GetForgotPasswordUrl()
                };
        }

        public AuthLoginPageModel Create(AuthLoginPostModel postModel)
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