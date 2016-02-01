using Core.UseCases;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.ChangePassword
{
    public class ChangePasswordConfirmationPageModel : AppPageModel
    {
        public ChangePasswordConfirmationPageModel(CoreContext.Result contextResult)
            : base("Password Changed", contextResult)
        {
        }
    }
}