using Application.UseCases.AppContext;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.ForgotPassword
{
    public class ForgotPasswordPageModel : AppPageModel
    {
        public string Email { get; set; }

        public ForgotPasswordPageModel(AppContextResult contextResult)
            : base("Forgot Password", contextResult)
        {
        }
    }
}