using Application.UseCases.UserList;
using Web.Models.UrlModels;

namespace Web.Models.UserModels.List
{
    public class UserListItemModel
    {
        public string Name { get; private set; }
        public UrlModel Url { get; private set; }

        public UserListItemModel(UserListItem userListItem)
        {
            Name = userListItem.DisplayName;
            Url = new UserDetailsUrlModel(userListItem.UserName);
        }
    }
}