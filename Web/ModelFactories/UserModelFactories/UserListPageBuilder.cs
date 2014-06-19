using Application.UseCases.AppContext;
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
            var contextResult = _appContextInteractor.Execute();
            var showUserListResult = _userListInteractor.Execute();

            return new UserListPageModel(
                contextResult,
                showUserListResult);
        }
    }
}
