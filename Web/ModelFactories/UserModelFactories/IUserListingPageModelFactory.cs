using System.Collections.Generic;
using Core.Classes;
using Web.Models.UserModels.Listing;

namespace Web.ModelFactories.UserModelFactories
{
    public interface IUserListingPageModelFactory
    {
        UserListingPageModel Create(User user, List<User> users);
    }
}