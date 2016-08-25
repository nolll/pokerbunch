using Core.UseCases;
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
    }
}