using System.Collections.Generic;
using Core.Settings;
using Core.UseCases;
using Web.Extensions;
using Web.Models.ErrorModels;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.ChangePassword
{
    public class ChangePasswordPageModel : AppPageModel
    {
        public ErrorListModel Errors { get; }

        public ChangePasswordPageModel(AppSettings appSettings, CoreContext.Result contextResult, IEnumerable<string> errors)
            : base(appSettings, contextResult)
        {
            Errors = new ErrorListModel(errors);
        }

        public override string BrowserTitle => "Change Password";

        public override View GetView()
        {
            return new View("~/Views/Pages/ChangePassword/ChangePassword.cshtml");
        }
    }
}