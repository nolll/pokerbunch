using Core.Classes;
using Web.Models.UserModels.ForgotPassword;

namespace Web.ModelFactories.UserModelFactories
{
    public interface IForgotPasswordPageModelFactory
    {
        ForgotPasswordPageModel Create(User user);
        ForgotPasswordConfirmationPageModel CreateConfirmation(User user);
        ForgotPasswordPageModel Create(User user, ForgotPasswordPostModel postModel);
    }
}