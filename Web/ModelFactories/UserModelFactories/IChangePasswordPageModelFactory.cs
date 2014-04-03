using Web.Models.UserModels.ChangePassword;

namespace Web.ModelFactories.UserModelFactories
{
    public interface IChangePasswordPageModelFactory
    {
        ChangePasswordPageModel Create();
        ChangePasswordConfirmationPageModel CreateConfirmation();
    }
}