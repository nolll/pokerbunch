using Core.UseCases;
using Web.Extensions;
using Web.Urls.SiteUrls;

namespace Web.Models.UserModels.List
{
    public class UserListItemModel : IViewModel
    {
        public string Name { get; private set; }
        public string Url { get; private set; }

        public UserListItemModel(UserList.UserListItem userListItem)
        {
            Name = userListItem.DisplayName;
            Url = new UserDetailsUrl(userListItem.UserName).Relative;
        }

        public View GetView()
        {
            return new View("UserList/Item");
        }
    }
}