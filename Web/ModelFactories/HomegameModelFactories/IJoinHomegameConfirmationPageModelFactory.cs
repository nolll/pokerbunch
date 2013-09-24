using Core.Classes;
using Web.Models.HomegameModels.Join;

namespace Web.ModelFactories.HomegameModelFactories
{
    public interface IJoinHomegameConfirmationPageModelFactory
    {
        JoinHomegameConfirmationPageModel Create(User user);
    }
}