using Application.Urls;
using Application.UseCases.AppContext;
using Web.Models.PageBaseModels;

namespace Web.Models.AuthModels
{
    public class LoginPageModel : AppPageModel
    {
        public Url AddUserUrl { get; private set; }
        public Url ForgotPasswordUrl { get; private set; }
        public string LoginName { get; private set; }
        public bool RememberMe { get; private set; }
        public string ReturnUrl { get; private set; }

        public LoginPageModel(AppContextResult contextResult, string returnUrl, LoginPostModel postModel)
            : base("Login", contextResult)
        {
            ReturnUrl = returnUrl != null ? new Url(returnUrl).Relative : new HomeUrl().Relative;
            AddUserUrl = new AddUserUrl();
            ForgotPasswordUrl = new ForgotPasswordUrl();
            if (postModel == null) return;
            LoginName = postModel.LoginName;
            RememberMe = postModel.RememberMe;
            ReturnUrl = postModel.ReturnUrl;
        }
    }
}