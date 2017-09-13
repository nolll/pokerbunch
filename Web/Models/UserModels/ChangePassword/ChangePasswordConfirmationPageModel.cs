using Core.UseCases;
using Web.Extensions;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.ChangePassword
{
    public class ChangePasswordConfirmationPageModel : AppPageModel
    {
        public ChangePasswordConfirmationPageModel(CoreContext.Result contextResult)
            : base(contextResult)
        {
        }

        public override string BrowserTitle => "Password Changed";

        public override View GetView()
        {
            return new View("~/Views/Pages/ChangePassword/ChangePasswordDone.cshtml");
        }
    }
}