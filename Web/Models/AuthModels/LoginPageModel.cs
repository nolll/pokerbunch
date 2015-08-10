using Core.Urls;
using Core.UseCases;
using Web.Models.PageBaseModels;
using Web.Urls;

namespace Web.Models.AuthModels
{
    public class LoginPageModel : AppPageModel
    {
        public string AddUserUrl { get; private set; }
        public string ForgotPasswordUrl { get; private set; }
        public string LoginName { get; private set; }
        public bool RememberMe { get; private set; }
        public string ReturnUrl { get; private set; }

        public LoginPageModel(AppContext.Result contextResult, LoginForm.Result loginFormResult, LoginPostModel postModel)
            : base("Login", contextResult)
        {
            ReturnUrl = loginFormResult.ReturnUrl;
            AddUserUrl = new AddUserUrl().Relative;
            ForgotPasswordUrl = new ForgotPasswordUrl().Relative;
            if (postModel == null) return;
            LoginName = postModel.LoginName;
            RememberMe = postModel.RememberMe;
            ReturnUrl = postModel.ReturnUrl;
        }
    }
}