using Core.Services;
using Infrastructure.System;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.AuthModels;

namespace Web.ModelFactories.AuthModelFactories
{
    public class AuthLoginPageModelFactory : IAuthLoginPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IWebContext _webContext;
        private readonly IUrlProvider _urlProvider;

        public AuthLoginPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory, 
            IWebContext webContext,
            IUrlProvider urlProvider)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _webContext = webContext;
            _urlProvider = urlProvider;
        }

        private AuthLoginPageModel Create()
        {
            var returnUrl = _webContext.GetQueryParam("return") ?? _urlProvider.GetHomeUrl();

            return new AuthLoginPageModel
                {
                    BrowserTitle = "Login",
                    PageProperties = _pagePropertiesFactory.Create(),
                    ReturnUrl = returnUrl,
                    AddUserUrl = _urlProvider.GetAddUserUrl(),
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