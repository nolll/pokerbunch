using Core.UseCases;
using Core.UseCases.UserList;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.UserModels.List;

namespace Web.ModelFactories.UserModelFactories
{
    public class UserListPageBuilder : IUserListPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IUserListItemModelFactory _userListItemModelFactory;
        private readonly IUserListInteractor _userListInteractor;

        public UserListPageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            IUserListItemModelFactory userListItemModelFactory,
            IUserListInteractor userListInteractor)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _userListItemModelFactory = userListItemModelFactory;
            _userListInteractor = userListInteractor;
        }

        public UserListPageModel Build()
        {
            var showUserListResult = _userListInteractor.Execute();

            return new UserListPageModel(
                "Users",
                _pagePropertiesFactory.Create(),
                _userListItemModelFactory.CreateList(showUserListResult.Users));
        }
    }
}
