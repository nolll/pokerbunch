using Core.Classes;
using Web.Models.UserModels;

namespace Web.ModelFactories.UserModelFactories
{
    public interface IUserDetailsPageModelFactory
    {
        UserDetailsPageModel Create(User currentUser, User displayUser);
    }
}