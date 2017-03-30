using Core.Services;
using Core.UseCases;

namespace Tests.Core.UseCases.LoginTests
{
    public abstract class Arrange : UseCaseTest<Login>
    {
        protected Login.Result Result;

        protected string ExistingUser => "existing-user";
        protected string UnknownUser => "unknow-user";
        protected string CorrectPassword => "correct-password";
        protected string WrongPassword => "wrong-password";
        private string Salt = "salt";
        protected virtual string LoginName => null;
        protected virtual string Password => null;
        protected string Token => "token";

        protected override void Setup()
        {
            Mock<ITokenService>().Setup(s => s.Get(ExistingUser, CorrectPassword)).Returns(Token);
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new Login.Request(LoginName, Password));
        }
    }
}
