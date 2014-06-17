using Core.Entities;
using Web.Models.UserModels;

namespace Web.ModelFactories.UserModelFactories
{
    public interface IUserDetailsPageBuilder
    {
        UserDetailsPageModel Build(User currentUser, User displayUser);
    }
}