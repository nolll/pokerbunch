using Application.UseCases.ApplicationContext;
using Application.UseCases.UserList;
using Web.Models.UserModels.List;

namespace Web.ModelFactories.UserModelFactories
{
    public class UserListPageBuilder : IUserListPageBuilder
    {
        private readonly IApplicationContextInteractor _applicationContextInteractor;
        private readonly IUserListInteractor _userListInteractor;

        public UserListPageBuilder(
            IApplicationContextInteractor applicationContextInteractor,
            IUserListInteractor userListInteractor)
        {
            _applicationContextInteractor = applicationContextInteractor;
            _userListInteractor = userListInteractor;
        }

        public UserListPageModel Build()
        {
            var applicationContextResult = _applicationContextInteractor.Execute();
            var showUserListResult = _userListInteractor.Execute();

            return new UserListPageModel(
                applicationContextResult,
                showUserListResult);
        }
    }
}
