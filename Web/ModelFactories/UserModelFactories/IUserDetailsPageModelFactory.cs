using Core.Entities;
using Web.Models.UserModels;

namespace Web.ModelFactories.UserModelFactories
{
    public interface IUserDetailsPageModelFactory
    {
        UserDetailsPageModel Create(User currentUser, User displayUser);
    }
}