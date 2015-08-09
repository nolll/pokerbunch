using Core.UseCases;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.ChangePassword
{
    public class ChangePasswordPageModel : AppPageModel
    {
        public ChangePasswordPageModel(AppContext.Result contextResult)
            : base("Change Password", contextResult)
        {
        }
    }
}