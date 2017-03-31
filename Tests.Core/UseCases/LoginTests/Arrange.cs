using Core.Entities;
using Core.Exceptions;
using Core.Services;
using Core.UseCases;
using Moq;

namespace Tests.Core.UseCases.LoginTests
{
    public abstract class Arrange : UseCaseTest<Login>
    {
        protected Login.Result Result;

        protected string ExistingUser => "existing-user";
        protected string UnknownUser => "unknown-user";
        protected string CorrectPassword => "correct-password";
        protected string WrongPassword => "wrong-password";
        private string Salt = "salt";
        protected virtual string LoginName => null;
        protected virtual string Password => null;
        protected string Token => "token";
        protected string UserId = "user-id";
        protected string UserName => ExistingUser;

        protected override void Setup()
        {
            Mock<IAuthService>().Setup(s => s.SignIn(ExistingUser, CorrectPassword)).Returns(Token);
            Mock<IAuthService>().Setup(s => s.SignIn(ExistingUser, WrongPassword)).Throws<LoginException>();
            Mock<IAuthService>().Setup(s => s.SignIn(UnknownUser, It.IsAny<string>())).Throws<LoginException>();
            Mock<IUserService>().Setup(o => o.Current(Token)).Returns(new User(UserId, UserName));
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new Login.Request(LoginName, Password));
        }
    }
}
