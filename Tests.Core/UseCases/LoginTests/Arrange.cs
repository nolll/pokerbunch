using Core.Entities;
using Core.Repositories;
using Core.Services;
using Core.UseCases;
using Moq;
using NUnit.Framework;

namespace Tests.Core.UseCases.LoginTests
{
    public class Arrange : UseCaseTest<Login>
    {
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

        [SetUp]
        public void Setup()
        {
            var user = new User(ExistingUserId, ExistingUser, "description", "real-name", "test@example.com", Role.None, EncryptedCorrectPassword, Salt);

            Mock<IUserRepository>().Setup(s => s.GetByNameOrEmail(ExistingUser)).Returns(user);
            Mock<ITokenRepository>().Setup(s => s.Get(ExistingUser, CorrectPassword)).Returns(Token);
        }

        protected Login.Result Execute()
        {
            return Sut.Execute(new Login.Request(LoginName, Password));
        }
    }
}
