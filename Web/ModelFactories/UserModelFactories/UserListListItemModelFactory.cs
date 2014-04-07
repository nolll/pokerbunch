using System.Collections.Generic;
using System.Linq;
using Application.Services;
using Core.UseCases;
using Web.Models.UserModels.List;

namespace Web.ModelFactories.UserModelFactories
{
    public class UserListListItemModelFactory : IUserListItemModelFactory
    {
        private readonly IUrlProvider _urlProvider;

        public UserListListItemModelFactory(IUrlProvider urlProvider)
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