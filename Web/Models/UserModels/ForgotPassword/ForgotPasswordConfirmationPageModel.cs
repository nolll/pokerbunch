using Core.Settings;
using Core.UseCases;
using Web.Extensions;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.ForgotPassword
{
    public class ForgotPasswordConfirmationPageModel : AppPageModel
    {
        public ForgotPasswordConfirmationPageModel(AppSettings appSettings, CoreContext.Result contextResult)
            : base(appSettings, contextResult)
        {
        }

        public override string BrowserTitle => "Password Sent";

        public override View GetView()
        {
            return new View("~/Views/Pages/ForgotPassword/ForgotPasswordDone.cshtml");
        }
    }
}