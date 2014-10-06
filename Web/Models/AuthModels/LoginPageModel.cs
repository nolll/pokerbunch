using Core.UseCases.AppContext;
using Core.UseCases.LoginForm;
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

        public LoginPageModel(AppContextResult contextResult, LoginFormResult loginFormResult, LoginPostModel postModel)
            : base("Login", contextResult)
        {
            ReturnUrl = loginFormResult.ReturnUrl.Relative;
            AddUserUrl = loginFormResult.AddUserUrl.Relative;
            ForgotPasswordUrl = loginFormResult.ForgotPasswordUrl.Relative;
            if (postModel == null) return;
            LoginName = postModel.LoginName;
            RememberMe = postModel.RememberMe;
            ReturnUrl = postModel.ReturnUrl;
        }
    }
}