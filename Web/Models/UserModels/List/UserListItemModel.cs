using Application.Urls;
using Application.UseCases.UserList;
using Web.Models.UrlModels;

namespace Web.Models.UserModels.List
{
    public class UserListItemModel
    {
        public string Name { get; private set; }
        public Url Url { get; private set; }

        public UserListItemModel(UserListItem userListItem)
        {
            Name = userListItem.DisplayName;
            Url = new UserDetailsUrl(userListItem.UserName);
        }
    }
}