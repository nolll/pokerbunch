using Application.UseCases.ApplicationContext;
using Application.UseCases.UserList;
using Web.Models.UserModels.List;

namespace Web.ModelFactories.UserModelFactories
{
    public class UserListPageBuilder : IUserListPageBuilder
    {
        private readonly IAppContextInteractor _appContextInteractor;
        private readonly IUserListInteractor _userListInteractor;

        public UserListPageBuilder(
            IAppContextInteractor appContextInteractor,
            IUserListInteractor userListInteractor)
        {
            _appContextInteractor = appContextInteractor;
            _userListInteractor = userListInteractor;
        }

        public UserListPageModel Build()
        {
            var applicationContextResult = _appContextInteractor.Execute();
            var showUserListResult = _userListInteractor.Execute();

            return new UserListPageModel(
                applicationContextResult,
                showUserListResult);
        }
    }
}
