using Core.Urls;
using Core.UseCases;

namespace Web.Models.UserModels.List
{
    public class UserListItemModel
    {
        public string Name { get; private set; }
        public string Url { get; private set; }

        public UserListItemModel(UserList.UserListItem userListItem)
        {
            Name = userListItem.DisplayName;
            Url = new UserDetailsUrl(userListItem.UserName).Relative;
        }
    }
}