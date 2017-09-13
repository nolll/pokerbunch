using Core.UseCases;
using Web.Extensions;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.ChangePassword
{
    public class ChangePasswordPageModel : AppPageModel
    {
        public ChangePasswordPageModel(CoreContext.Result contextResult)
            : base(contextResult)
        {
        }

        public override string BrowserTitle => "Change Password";

        public override View GetView()
        {
            return new View("~/Views/Pages/ChangePassword/ChangePassword.cshtml");
        }
    }
}