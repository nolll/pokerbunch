using Core.UseCases.UserList;

namespace Web.Models.UserModels.List
{
    public class UserListItemModel
    {
        public string Name { get; private set; }
        public string Url { get; private set; }

        public UserListItemModel(UserListItem userListItem)
        {
            Name = userListItem.DisplayName;
            Url = userListItem.Url.Relative;
        }
    }
}