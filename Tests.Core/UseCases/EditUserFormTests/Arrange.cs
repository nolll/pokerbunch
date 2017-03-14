using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.EditUserFormTests
{
    public abstract class Arrange : UseCaseTest<EditUserForm>
    {
        protected EditUserForm.Result Result;

        private const string UserId = UserData.Id1;
        protected const string UserName = UserData.UserName1;
        protected const string RealName = "real-name";
        protected const string DisplayName = UserData.DisplayName1;
        protected const string Email = "email@example.com";

        protected override void Setup()
        {
            var user = new User(UserId, UserName, DisplayName, RealName, Email);

            Mock<IUserRepository>().Setup(o => o.GetByNameOrEmail(UserName)).Returns(user);
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new EditUserForm.Request(UserName));
        }
    }
}