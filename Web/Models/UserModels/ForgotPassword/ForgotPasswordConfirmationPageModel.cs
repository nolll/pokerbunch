using Core.UseCases;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.ForgotPassword
{
    public class ForgotPasswordConfirmationPageModel : AppPageModel
    {
        public ForgotPasswordConfirmationPageModel(AppContext.Result contextResult)
            : base("Password Sent", contextResult)
        {
        }
    }
}