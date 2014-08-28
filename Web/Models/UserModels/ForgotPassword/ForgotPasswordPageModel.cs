using Application.UseCases.AppContext;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.ForgotPassword
{
    public class ForgotPasswordPageModel : AppPageModel
    {
        public string Email { get; private set; }

        public ForgotPasswordPageModel(AppContextResult contextResult, ForgotPasswordPostModel postModel)
            : base("Forgot Password", contextResult)
        {
            if (postModel == null) return;
            Email = postModel.Email;
        }
    }
}