using Core.UseCases;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.UserModels.List;

namespace Web.ModelFactories.UserModelFactories
{
    public class UserListPageBuilder : IUserListPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IUserListItemModelFactory _userListItemModelFactory;
        private readonly IShowUserList _showUserList;

        public UserListPageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            IUserListItemModelFactory userListItemModelFactory,
            IShowUserList showUserList)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _userListItemModelFactory = userListItemModelFactory;
            _showUserList = showUserList;
        }

        public UserListPageModel Build()
        {
            var showUserListResult = _showUserList.Execute();

            return new UserListPageModel(
                "Users",
                _pagePropertiesFactory.Create(),
                _userListItemModelFactory.CreateList(showUserListResult.Users));
        }
    }
}
