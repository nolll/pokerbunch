using System.Collections.Generic;
using Application.UseCases.UserList;
using Web.Models.UserModels.List;

namespace Web.ModelFactories.UserModelFactories
{
    public interface IUserListItemModelFactory
    {
        UserListItemModel Create(UserListItem userListItem);
        IList<UserListItemModel> CreateList(IList<UserListItem> userListItems);
    }
}