using Web.Models.AuthModels;
using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.ModelFactories.AuthModelFactories
{
    public class AuthLoginPageModelFactory : IAuthLoginPageModelFactory
    {
        public AuthLoginPageModel Create(string returnUrl, string loginName)
        {
            return new AuthLoginPageModel
                {
                    BrowserTitle = "Login",
                    PageProperties = new PageProperties(null),
                    ReturnUrl = returnUrl ?? new HomeUrlModel().Url,
                    AddUserUrl = new UserAddUrlModel(),
                    ForgotPasswordUrl = new ForgotPasswordUrlModel(),
                    LoginName = loginName
                };
        }
    }
}