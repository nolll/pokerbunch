using System.Collections.Generic;
using System.Linq;
using Application.Services;
using Application.UseCases.UserList;
using Web.Models.UserModels.List;

namespace Web.ModelFactories.UserModelFactories
{
    public class UserListItemModelFactory : IUserListItemModelFactory
    {
        private readonly IUrlProvider _urlProvider;

        public UserListItemModelFactory(IUrlProvider urlProvider)
        {
            _urlProvider = urlProvider;
        }

        public UserListItemModel Create(UserListItem userListItem)
        {
            return new UserListItemModel
                {
                    Name = userListItem.DisplayName,
                    Url = _urlProvider.GetUserDetailsUrl(userListItem.UserName)
                };
        }

        public IList<UserListItemModel> CreateList(IList<UserListItem> userListItems)
        {
            return userListItems.Select(Create).ToList();
        }
    }
}