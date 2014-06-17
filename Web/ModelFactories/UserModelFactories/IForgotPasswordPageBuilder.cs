using Web.Models.UserModels.ForgotPassword;

namespace Web.ModelFactories.UserModelFactories
{
    public interface IForgotPasswordPageBuilder
    {
        ForgotPasswordConfirmationPageModel BuildConfirmation();
        ForgotPasswordPageModel Build(ForgotPasswordPostModel postModel = null);
    }
}