using Application.UseCases.AppContext;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.ForgotPassword
{
    public class ForgotPasswordConfirmationPageModel : PageModel
    {
        public ForgotPasswordConfirmationPageModel(AppContextResult contextResult)
            : base("Password Sent", contextResult)
        {
        }
    }
}