using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.AuthModels;
using Web.Models.UrlModels;

namespace Web.ModelFactories.AuthModelFactories
{
    public class AuthLoginPageModelFactory : IAuthLoginPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public AuthLoginPageModelFactory(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public AuthLoginPageModel Create(string returnUrl, string loginName)
        {
            return new AuthLoginPageModel
                {
                    BrowserTitle = "Login",
                    PageProperties = _pagePropertiesFactory.Create(),
                    ReturnUrl = returnUrl ?? new HomeUrlModel().Url,
                    AddUserUrl = new UserAddUrlModel(),
                    ForgotPasswordUrl = new ForgotPasswordUrlModel(),
                    LoginName = loginName
                };
        }
    }
}