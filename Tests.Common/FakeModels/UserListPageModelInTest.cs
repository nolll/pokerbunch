using Application.UseCases.AppContext;
using Application.UseCases.UserList;
using Tests.Common.FakeClasses;
using Web.Models.UserModels.List;

namespace Tests.Common.FakeModels
{
    public class UserListPageModelInTest : UserListPageModel
    {
        public UserListPageModelInTest(
            AppContextResult contextResult = null,
            UserListResult userListResult = null)
            
            : base(
                contextResult ?? new AppContextResultInTest(),
                userListResult ?? new UserListResultInTest())
        {
        }
    }
}