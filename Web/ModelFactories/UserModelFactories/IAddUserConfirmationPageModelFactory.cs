using Core.Classes;
using Web.Models.UserModels.Add;

namespace Web.ModelFactories.UserModelFactories
{
    public interface IAddUserConfirmationPageModelFactory
    {
        AddUserConfirmationPageModel Create(User user);
    }
}