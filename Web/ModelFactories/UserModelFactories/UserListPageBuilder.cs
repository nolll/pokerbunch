using Application.UseCases.AppContext;
using Application.UseCases.UserList;
using Web.Models.UserModels.List;

namespace Web.ModelFactories.UserModelFactories
{
    public class UserListPageBuilder : IUserListPageBuilder
    {
        private readonly IAppContextInteractor _contextInteractor;
        private readonly IUserListInteractor _userListInteractor;

        public UserListPageBuilder(
            IAppContextInteractor contextInteractor,
            IUserListInteractor userListInteractor)
        {
            _contextInteractor = contextInteractor;
            _userListInteractor = userListInteractor;
        }

        public UserListPageModel Build()
        {
            var contextResult = _contextInteractor.Execute();
            var showUserListResult = _userListInteractor.Execute();

            return new UserListPageModel(
                contextResult,
                showUserListResult);
        }
    }
}
