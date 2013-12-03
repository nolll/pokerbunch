using System.Collections.Generic;
using Core.Classes;
using Web.Models.UserModels.List;

namespace Web.ModelFactories.UserModelFactories
{
    public interface IUserListPageModelFactory
    {
        UserListPageModel Create(User user, IList<User> users);
    }
}