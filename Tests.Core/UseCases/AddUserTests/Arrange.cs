using Core.Entities;
using Core.Services;
using Core.UseCases;
using Moq;
using Tests.Core.Data;

namespace Tests.Core.UseCases.AddUserTests
{
    public abstract class Arrange : UseCaseTest<AddUser>
    {
        protected User Added;
        protected string SentPassword;

        protected const string UserName = UserData.UserName1;
        protected const string DisplayName = UserData.DisplayName1;
        protected const string Email = UserData.Email1;
        protected const string Password = "password";

        protected override void Setup()
        {
            Added = null;
            SentPassword = null;

            Mock<IUserService>().Setup(o => o.Add(It.IsAny<User>(), Password))
                .Callback((User u, string password) => { Added = u; SentPassword = password; });
        }

        protected override void Execute()
        {
            Subject.Execute(new AddUser.Request(UserName, DisplayName, Email, Password));
        }
    }
}