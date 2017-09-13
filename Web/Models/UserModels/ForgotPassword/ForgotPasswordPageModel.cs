using Core.UseCases;
using Web.Extensions;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.ForgotPassword
{
    public class ForgotPasswordPageModel : AppPageModel
    {
        public string Email { get; private set; }

        public ForgotPasswordPageModel(CoreContext.Result contextResult, ForgotPasswordPostModel postModel)
            : base(contextResult)
        {
            if (postModel == null) return;
            Email = postModel.Email;
        }

        public override string BrowserTitle => "Forgot Password";

        public override View GetView()
        {
            return new View("~/Views/Pages/ForgotPassword/ForgotPassword.cshtml");
        }
    }
}