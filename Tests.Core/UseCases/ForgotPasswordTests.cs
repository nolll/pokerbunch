using System.Linq;
using Core;
using Core.Exceptions;
using Core.Services;
using Core.Urls;
using Core.UseCases.ForgotPassword;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class ForgotPasswordTests : TestBase
    {
        private const string ValidEmail = Constants.UserEmailA;
        private const string InvalidEmail = "";
        private const string NonExistingEmail = "a@b.com";

        [Test]
        public void ForgotPassword_WithValidEmail_ReturnUrlIsSet()
        {
            var result = Execute(CreateRequest());

            Assert.IsInstanceOf<ForgotPasswordConfirmationUrl>(result.ReturnUrl);
        }

        [Test]
        public void ForgotPassword_WithInvalidEmail_ValidationExceptionIsThrown()
        {
            var request = CreateRequest(InvalidEmail);

            var ex = Assert.Throws<ValidationException>(() => Execute(request));
            Assert.AreEqual(1, ex.Messages.Count());
        }

        [Test]
        public void ForgotPassword_UserNotFound_ThrowsException()
        {
            Assert.Throws<UserNotFoundException>(() => Execute(CreateRequest(NonExistingEmail)));
        }

        [Test]
        public void ForgotPassword_SendsPasswordEmail()
        {
            const string subject = "Poker Bunch Password Recovery";
            const string body = @"Here is your new password for Poker Bunch:
aaaaaaaa

Please sign in here: http://pokerbunch.com/-/auth/login";
            SetupRandomCharacters();

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
            SetupRandomCharacters();

            Execute(CreateRequest());

            var savedUser = Repos.User.Saved;
            Assert.AreEqual("0478095c8ece0bbc11f94663ac2c4f10b29666de", savedUser.EncryptedPassword);
            Assert.AreEqual("aaaaaaaaaa", savedUser.Salt);
        }

        private void SetupRandomCharacters()
        {
            GetMock<IRandomService>().Setup(o => o.GetAllowedChars()).Returns("a");
        }
        
        private ForgotPasswordRequest CreateRequest(string email = ValidEmail)
        {
            return new ForgotPasswordRequest(email);
        }

        private ForgotPasswordResult Execute(ForgotPasswordRequest request)
        {
            return ForgotPasswordInteractor.Execute(
                Repos.User,
                GetMock<IMessageSender>().Object,
                GetMock<IRandomService>().Object,
                request);
        }
    }
}
