using System.Linq;
using Core;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Services.Interfaces;
using Core.Urls;
using Core.UseCases.ForgotPassword;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
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
        public void ForgotPassword_SendsPasswordEmail()
        {
            const string subject = "Poker Bunch Password Recovery";
            const string body = @"Here is your new password for Poker Bunch:
aaaaaaaa

Please sign in here: http://pokerbunch.com/-/auth/login";
            SetupUser();
            SetupPasswordCharacters();

            IMessage message = null;
            string email = null;
            GetMock<IMessageSender>()
                .Setup(o => o.Send(It.IsAny<string>(), It.IsAny<IMessage>()))
                .Callback((string e, IMessage m) =>
                {
                    email = e;
                    message = m;
                });

            Execute(CreateRequest());

            Assert.AreEqual(ValidEmail, email);
            Assert.AreEqual(subject, message.Subject);
            Assert.AreEqual(body, message.Body);
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

            Assert.AreEqual("f946ba8dd6db9197cf82bbdab303a4d05316384e", savedUser.EncryptedPassword);
            Assert.AreEqual("bbbbbbbbbb", savedUser.Salt);
        }

        private void SetupPasswordCharacters()
        {
            GetMock<IRandomService>().Setup(o => o.GetPasswordCharacters()).Returns("a");
        }

        private void SetupSaltCharacters()
        {
            GetMock<IRandomService>().Setup(o => o.GetSaltCharacters()).Returns("b");
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
