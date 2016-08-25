using Core.UseCases;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.ForgotPassword
{
    public class ForgotPasswordConfirmationPageModel : AppPageModel
    {
        public ForgotPasswordConfirmationPageModel(CoreContext.Result contextResult)
            : base(contextResult)
        {
        }

        public override string BrowserTitle => "Password Sent";
    }
}