using System.Collections.Generic;
using System.Linq;
using Core.Classes;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.UserModels.Listing;

namespace Web.ModelFactories.UserModelFactories
{
    public class UserListingPageModelFactory : IUserListingPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public UserListingPageModelFactory(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public UserListingPageModel Create(User user, IList<User> users)
        {
            return new UserListingPageModel
                {
                    BrowserTitle = "Users",
                    PageProperties = _pagePropertiesFactory.Create(user),
                    UserModels = GetUserModels(users)
                };
        }
        
        private List<UserItemModel> GetUserModels(IEnumerable<User> users)
        {
            return users.Select(user => new UserItemModel(user)).ToList();
        }
    }
}
