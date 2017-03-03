using Core.Entities;
using Core.Repositories;
using Core.UseCases;

namespace Tests.Core.UseCases.UserDetailsTests
{
    public class Arrange : UseCaseTest<UserDetails>
    {
        protected UserDetails.Result Result;

        private const string CurrentUserId = "1";
        private const string ViewUserId = "2";
        private string _currentUserName = "currentusername";
        protected const string ViewUserName = "viewusername";
        protected const string DisplayName = "displayname";
        protected const string RealName = "realname";
        protected const string Email = "email";
        protected virtual Role Role => Role.Player;
        protected virtual bool ViewingOwnUser => false; 

        protected override void Setup()
        {
            if (ViewingOwnUser)
            {
                Mock<IUserRepository>().Setup(s => s.GetByNameOrEmail(ViewUserName)).Returns(new User(ViewUserId, ViewUserName, DisplayName, RealName, Email, Role));
                _currentUserName = ViewUserName;
            }
            else
            {
                Mock<IUserRepository>().Setup(s => s.GetByNameOrEmail(_currentUserName)).Returns(new User(CurrentUserId, _currentUserName, globalRole: Role));
                Mock<IUserRepository>().Setup(s => s.GetByNameOrEmail(ViewUserName)).Returns(new User(ViewUserId, ViewUserName, DisplayName, RealName, Email, Role));
            }
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new UserDetails.Request(_currentUserName, ViewUserName));
        }
    }
}
