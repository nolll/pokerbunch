using Core.UseCases;
using Core.UseCases.ShowUserList;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.UserModels.List;

namespace Web.ModelFactories.UserModelFactories
{
    public class UserListPageBuilder : IUserListPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IUserListItemModelFactory _userListItemModelFactory;
        private readonly IShowUserListInteractor _showUserListInteractor;

        public UserListPageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            IUserListItemModelFactory userListItemModelFactory,
            IShowUserListInteractor showUserListInteractor)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _userListItemModelFactory = userListItemModelFactory;
            _showUserListInteractor = showUserListInteractor;
        }

        public UserListPageModel Build()
        {
            var showUserListResult = _showUserListInteractor.Execute();

            return new UserListPageModel(
                "Users",
                _pagePropertiesFactory.Create(),
                _userListItemModelFactory.CreateList(showUserListResult.Users));
        }
    }
}
