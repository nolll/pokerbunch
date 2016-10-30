using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases.LoginTests
{
    public class Arrange : ArrangeBase
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
        private Login Sut { get; set; }

        [SetUp]
        public void Setup()
        {
            var user = new User(ExistingUserId, ExistingUser, "description", "real-name", "test@example.com", Role.None, EncryptedCorrectPassword, Salt);

            var userRepositoryMock = new Mock<IUserRepository>();
            var tokenRepositoryMock = new Mock<ITokenRepository>();
            userRepositoryMock.Setup(s => s.GetByNameOrEmail(ExistingUser)).Returns(user);
            tokenRepositoryMock.Setup(s => s.Get(ExistingUser, CorrectPassword)).Returns(Token);

            Sut = new Login(new UserService(userRepositoryMock.Object), tokenRepositoryMock.Object);
        }

        protected Login.Result Execute()
        {
            return Sut.Execute(new Login.Request(LoginName, Password));
        }

        //[Test]
        //public void Login_UserFoundAndPasswordIsCorrect_UserNameIsSet()
        //{
        //    var result = Sut.Execute(CreateRequest());

        //    Assert.AreEqual(TestData.UserA.UserName, result.UserName);
        //}
    }
}
