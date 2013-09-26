using System.Collections.Generic;
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

        public UserListingPageModel Create(User user, List<User> users)
        {
            return new UserListingPageModel
                {
                    BrowserTitle = "Users",
                    PageProperties = _pagePropertiesFactory.Create(user),
                    UserModels = GetUserModels(users)
                };
        }
        
        private List<UserItemModel> GetUserModels(IEnumerable<User> users){
			var models = new List<UserItemModel>();
			foreach(var user in users){
				models.Add(new UserItemModel(user));
			}
			return models;
		}

    }
}
