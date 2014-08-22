using Application.UseCases.AppContext;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.ChangePassword
{
    public class ChangePasswordConfirmationPageModel : AppPageModel
    {
        public ChangePasswordConfirmationPageModel(AppContextResult contextResult)
            : base("Password Changed", contextResult)
        {
        }
    }
}