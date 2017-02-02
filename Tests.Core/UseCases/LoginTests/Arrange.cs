using Core.Entities;
using Core.Repositories;
using Core.Services;
using Core.UseCases;
using Moq;
using NUnit.Framework;

namespace Tests.Core.UseCases.LoginTests
{
    public class Arrange
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
        protected Login Sut;

        [SetUp]
        public void Setup()
        {
            var user = new User(ExistingUserId, ExistingUser, "description", "real-name", "test@example.com", Role.None, EncryptedCorrectPassword, Salt);

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(s => s.GetByNameOrEmail(ExistingUser)).Returns(user);

            var tokenRepositoryMock = new Mock<ITokenRepository>();
            tokenRepositoryMock.Setup(s => s.Get(ExistingUser, CorrectPassword)).Returns(Token);

            Sut = new Login(userRepositoryMock.Object, tokenRepositoryMock.Object);
        }

        protected Login.Request Request => new Login.Request(LoginName, Password);

        //[Test]
        //public void Login_UserFoundAndPasswordIsCorrect_UserNameIsSet()
        //{
        //    var result = Sut.Execute(CreateRequest());

        //    Assert.AreEqual(TestData.UserA.UserName, result.UserName);
        //}
    }
}
