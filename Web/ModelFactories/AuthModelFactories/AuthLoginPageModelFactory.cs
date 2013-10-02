using Infrastructure.System;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.AuthModels;
using Web.Models.UrlModels;

namespace Web.ModelFactories.AuthModelFactories
{
    public class AuthLoginPageModelFactory : IAuthLoginPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IWebContext _webContext;

        public AuthLoginPageModelFactory(IPagePropertiesFactory pagePropertiesFactory, IWebContext webContext)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _webContext = webContext;
        }

        public AuthLoginPageModel Create()
        {
            var returnUrl = _webContext.GetQueryParam("return") ?? new HomeUrlModel().Url;

            return new AuthLoginPageModel
                {
                    BrowserTitle = "Login",
                    PageProperties = _pagePropertiesFactory.Create(),
                    ReturnUrl = returnUrl,
                    AddUserUrl = new UserAddUrlModel(),
                    ForgotPasswordUrl = new ForgotPasswordUrlModel(),
                };
        }

        public AuthLoginPageModel Create(AuthLoginPostModel postModel)
        {
            var model = Create();
            model.LoginName = postModel.LoginName;
            model.RememberMe = postModel.RememberMe;
            model.ReturnUrl = postModel.ReturnUrl;
            return model;
        }
    }
}