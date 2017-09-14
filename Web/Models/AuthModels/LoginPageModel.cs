using System.Collections.Generic;
using Core.UseCases;
using Web.Extensions;
using Web.Models.ErrorModels;
using Web.Models.PageBaseModels;
using Web.Urls.SiteUrls;

namespace Web.Models.AuthModels
{
    public class LoginPageModel : AppPageModel
    {
        public string AddUserUrl { get; private set; }
        public string ForgotPasswordUrl { get; private set; }
        public string LoginName { get; private set; }
        public bool RememberMe { get; private set; }
        public string ReturnUrl { get; private set; }
        public ErrorListModel Errors { get; }

        public LoginPageModel(CoreContext.Result contextResult, LoginForm.Result loginFormResult, LoginPostModel postModel, IEnumerable<string> errors)
            : base(contextResult)
        {
            ReturnUrl = loginFormResult.ReturnUrl;
            AddUserUrl = new AddUserUrl().Relative;
            ForgotPasswordUrl = new ForgotPasswordUrl().Relative;
            if (postModel == null) return;
            LoginName = postModel.LoginName;
            RememberMe = postModel.RememberMe;
            ReturnUrl = postModel.ReturnUrl;
            Errors = new ErrorListModel(errors);
        }

        public override string BrowserTitle => "Login";

        public override View GetView()
        {
            return new View("~/Views/Login/Login.cshtml");
        }
    }
}