using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Extensions;

namespace Web.Models.UserModels.List
{
    public class UserListItemModel : IViewModel
    {
        public string Name { get; }
        public string Url { get; }

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