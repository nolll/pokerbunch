using Application.UseCases.AppContext;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.ChangePassword
{
    public class ChangePasswordPageModel : AppPageModel
    {
        public ChangePasswordPageModel(AppContextResult contextResult)
            : base("Change Password", contextResult)
        {
        }
    }
}