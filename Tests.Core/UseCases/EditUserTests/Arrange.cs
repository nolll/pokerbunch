using Core.Entities;
using Core.Services;
using Core.UseCases;
using Moq;
using Tests.Core.Data;

namespace Tests.Core.UseCases.EditUserTests
{
    public abstract class Arrange : UseCaseTest<EditUser>
    {
        protected EditUser.Result Result;

        protected User PostedUser;

        private const string UserId = UserData.Id1;
        protected const string UserName = UserData.UserName1;
        private const string DisplayName = UserData.DisplayName1;
        private const string RealName = "real-name-1";
        private const string Email = UserData.Email1;

        protected const string ChangedDisplayName = UserData.DisplayName2;
        protected const string ChangedRealName = "real-name-2";
        protected const string ChangedEmail = UserData.Email2;

        protected override void Setup()
        {
            PostedUser = null;

            var existingUser = new User(UserId, UserName, DisplayName, RealName, Email);

            Mock<IUserService>().Setup(o => o.GetByNameOrEmail(UserName)).Returns(existingUser);
            Mock<IUserService>().Setup(o => o.Update(It.IsAny<User>())).Callback((User user) => PostedUser = user);
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new EditUser.Request(UserName, ChangedDisplayName, ChangedRealName, ChangedEmail));
        }
    }
}