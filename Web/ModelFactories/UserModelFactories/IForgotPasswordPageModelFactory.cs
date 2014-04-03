using Web.Models.UserModels.ForgotPassword;

namespace Web.ModelFactories.UserModelFactories
{
    public interface IForgotPasswordPageModelFactory
    {
        ForgotPasswordConfirmationPageModel CreateConfirmation();
        ForgotPasswordPageModel Create(ForgotPasswordPostModel postModel);
    }
}