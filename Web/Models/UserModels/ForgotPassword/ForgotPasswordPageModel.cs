using System.Collections.Generic;
using Core.Settings;
using Core.UseCases;
using Web.Extensions;
using Web.Models.ErrorModels;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.ForgotPassword
{
    public class ForgotPasswordPageModel : AppPageModel
    {
        public string Email { get; }
        public ErrorListModel Errors { get; }

        public ForgotPasswordPageModel(AppSettings appSettings, CoreContext.Result contextResult, ForgotPasswordPostModel postModel, IEnumerable<string> errors)
            : base(appSettings, contextResult)
        {
            if (postModel == null) return;
            Email = postModel.Email;
            Errors = new ErrorListModel(errors);
        }

        public override string BrowserTitle => "Forgot Password";

        public override View GetView()
        {
            return new View("~/Views/Pages/ForgotPassword/ForgotPassword.cshtml");
        }
    }
}