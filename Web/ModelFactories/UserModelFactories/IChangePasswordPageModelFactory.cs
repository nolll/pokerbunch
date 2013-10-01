using Core.Classes;
using Web.Models.UserModels.ChangePassword;

namespace Web.ModelFactories.UserModelFactories
{
    public interface IChangePasswordPageModelFactory
    {
        ChangePasswordPageModel Create(User user);
        ChangePasswordConfirmationPageModel CreateConfirmation(User user);
    }
}