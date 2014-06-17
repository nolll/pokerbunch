using Web.Models.UserModels.ChangePassword;

namespace Web.ModelFactories.UserModelFactories
{
    public interface IChangePasswordPageBuilder
    {
        ChangePasswordPageModel Build();
        ChangePasswordConfirmationPageModel BuildConfirmation();
    }
}