using System.Linq;
using Application.Exceptions;
using Application.Services;
using Application.Urls;
using Application.UseCases.ForgotPassword;
using Core.Entities;
using Core.Repositories;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Application.UseCases
{
    class ForgotPasswordTests : TestBase
    {
        private const string ValidEmail = "a@b.com";

        [Test]
        public void ForgotPassword_WithValidEmail_ReturnUrlIsSet()
        {
            SetupUser();

            var result = Execute(CreateRequest());

            Assert.IsInstanceOf<ForgotPasswordConfirmationUrl>(result.ReturnUrl);
        }

        [Test]
        public void ForgotPassword_WithInvalidEmail_ValidationExceptionIsThrown()
        {
            const string email = "";
            var request = CreateRequest(email);

            var ex = Assert.Throws<ValidationException>(() => Execute(request));
            Assert.AreEqual(1, ex.Messages.Count());
        }

        [Test]
        public void ForgotPassword_UserNotFound_ThrowsException()
        {
            Assert.Throws<UserNotFoundException>(() => Execute(CreateRequest()));
        }

        [Test]
        public void ForgotPassword_SavesNewPassword()
        {

        }

        [Test]
        public void ForgotPassword_SendsPasswordEmail()
        {
            const string subject = "Poker Bunch password recovery";
            const string body = @"Here is your new password for Poker Bunch:
aaaaaaaa

Please sign in here: http://pokerbunch.com/-/auth/login";
            SetupUser();
            SetupPasswordCharacters();

            Execute(CreateRequest());

            GetMock<IMessageSender>().Verify(o => o.Send(ValidEmail, subject, body));
        }

        [Test]
        public void ForgotPassword_SavesUserWithNewPassword()
        {
            SetupUser();
            SetupPasswordCharacters();
            SetupSaltCharacters();

            User savedUser = null;
            GetMock<IUserRepository>().Setup(o => o.Save(It.IsAny<User>())).Callback((User user) => savedUser = user);

            Execute(CreateRequest());

            Assert.AreEqual("63ff91274d5b2ed6c6e694bab8a02bce48078405", savedUser.EncryptedPassword);
            Assert.AreEqual("bbbbbbbbbb", savedUser.Salt);
        }

        private void SetupPasswordCharacters()
        {
            GetMock<IRandomService>().Setup(o => o.GetPasswordCharacters()).Returns("a");
        }

        private void SetupSaltCharacters()
        {
            GetMock<IRandomService>().Setup(o => o.GetPasswordCharacters()).Returns("b");
        }

        private ForgotPasswordRequest CreateRequest(string email = ValidEmail)
        {
            return new ForgotPasswordRequest(email);
        }
        
        private void SetupUser()
        {
            var user = A.User.Build();
            GetMock<IUserRepository>().Setup(o => o.GetByNameOrEmail(ValidEmail)).Returns(user);
        }

        private ForgotPasswordResult Execute(ForgotPasswordRequest request)
        {
            return ForgotPasswordInteractor.Execute(
                GetMock<IUserRepository>().Object,
                GetMock<IMessageSender>().Object,
                GetMock<IRandomService>().Object,
                request);
        }
    }
}
