using Core.Entities;
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
        private string EncryptedCorrectPassword => EncryptionService.Encrypt(CorrectPassword, Salt);
        protected string WrongPassword => "wrong-password";
        private string Salt = "salt";
        private string ExistingUserId => "1";
        protected virtual string LoginName => null;
        protected virtual string Password => null;
        protected string Token => "token";

        protected override void Setup()
        {
            var user = new User(ExistingUserId, ExistingUser, "description", "real-name", "test@example.com", Role.None, EncryptedCorrectPassword, Salt);

            Mock<IUserService>().Setup(s => s.GetByNameOrEmail(ExistingUser)).Returns(user);
            Mock<ITokenService>().Setup(s => s.Get(ExistingUser, CorrectPassword)).Returns(Token);
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new Login.Request(LoginName, Password));
        }
    }
}
