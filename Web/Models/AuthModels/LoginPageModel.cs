using Core.UseCases;
using Web.Common.Urls.SiteUrls;
using Web.Models.PageBaseModels;

namespace Web.Models.AuthModels
{
    public class LoginPageModel : AppPageModel
    {
        public string AddUserUrl { get; private set; }
        public string ForgotPasswordUrl { get; private set; }
        public string LoginName { get; private set; }
        public bool RememberMe { get; private set; }
        public string ReturnUrl { get; private set; }

        public LoginPageModel(CoreContext.Result contextResult, LoginForm.Result loginFormResult, LoginPostModel postModel)
            : base(contextResult)
        {
            ReturnUrl = loginFormResult.ReturnUrl;
            AddUserUrl = new AddUserUrl().Relative;
            ForgotPasswordUrl = new ForgotPasswordUrl().Relative;
            if (postModel == null) return;
            LoginName = postModel.LoginName;
            RememberMe = postModel.RememberMe;
            ReturnUrl = postModel.ReturnUrl;
        }

        public override string BrowserTitle => "Login";
    }
}