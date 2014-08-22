using Application.UseCases.AppContext;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.ForgotPassword
{
    public class ForgotPasswordConfirmationPageModel : AppPageModel
    {
        public ForgotPasswordConfirmationPageModel(AppContextResult contextResult)
            : base("Password Sent", contextResult)
        {
        }
    }
}