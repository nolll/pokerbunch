using System.Collections.Generic;
using System.Linq;
using Core.Classes;
using Core.UseCases;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.UserModels.List;

namespace Web.ModelFactories.UserModelFactories
{
    public class UserListPageModelFactory : IUserListPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IUserItemModelFactory _userItemModelFactory;

        public UserListPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            IUserItemModelFactory userItemModelFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _userItemModelFactory = userItemModelFactory;
        }

        public UserListPageModel Create(User user, ShowUserListResult showUserListResult)
        {
            return new UserListPageModel(
                    "Users",
                    _pagePropertiesFactory.Create(user),
                    GetUserModels(showUserListResult.Users)
                );
        }
        
        private List<UserItemModel> GetUserModels(IEnumerable<UserItem> users)
        {
            return users.Select(_userItemModelFactory.Create).ToList();
        }
    }
}
