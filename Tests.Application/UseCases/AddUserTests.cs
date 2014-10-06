using System.Linq;
using Core;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Services.Interfaces;
using Core.Urls;
using Core.UseCases.AddUser;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Application.UseCases
{
    class AddUserTests : TestBase
    {
        private const string ValidUserName = "a";
        private const string ValidDisplayName = "b";
        private const string ValidEmail = "a@b.com";

        [Test]
        public void AddUser_ReturnUrlIsSet()
        {
            var request = new AddUserRequest(ValidUserName, ValidDisplayName, ValidEmail);

            var result = Execute(request);

            Assert.IsInstanceOf<AddUserConfirmationUrl>(result.ReturnUrl);
        }

        [Test]
        public void AddUser_WithEmptyUserName_ThrowsValidationError()
        {
            var request = new AddUserRequest("", ValidDisplayName, ValidEmail);

            var ex = Assert.Throws<ValidationException>(() => Execute(request));
            Assert.AreEqual(1, ex.Messages.Count());
        }

        [Test]
        public void AddUser_WithEmptyDisplayName_ThrowsValidationError()
        {
            var request = new AddUserRequest(ValidUserName, "", ValidEmail);

            var ex = Assert.Throws<ValidationException>(() => Execute(request));
            Assert.AreEqual(1, ex.Messages.Count());
        }

        [Test]
        public void AddUser_WithEmptyEmail_ThrowsValidationError()
        {
            var request = new AddUserRequest(ValidUserName, ValidDisplayName, "");

            var ex = Assert.Throws<ValidationException>(() => Execute(request));
            Assert.AreEqual(1, ex.Messages.Count());
        }

        [Test]
        public void AddUser_UserNameAlreadyInUse_ThrowsException()
        {
            var request = new AddUserRequest(ValidUserName, ValidDisplayName, ValidEmail);

            GetMock<IUserRepository>().Setup(o => o.GetByNameOrEmail(ValidUserName)).Returns(A.User.Build());

            Assert.Throws<UserExistsException>(() => Execute(request));
        }

        [Test]
        public void AddUser_EmailAlreadyInUse_ThrowsException()
        {
            var request = new AddUserRequest(ValidUserName, ValidDisplayName, ValidEmail);

            GetMock<IUserRepository>().Setup(o => o.GetByNameOrEmail(ValidEmail)).Returns(A.User.Build());

            Assert.Throws<EmailExistsException>(() => Execute(request));
        }

        [Test]
        public void AddUser_WithValidInput_UserWithCorrectPropertiesIsAdded()
        {
            const string expectedEncryptedPassword = "f946ba8dd6db9197cf82bbdab303a4d05316384e";
            const string expectedSalt = "bbbbbbbbbb";

            SetupPasswordCharacters();
            SetupSaltCharacters();
            User user = null;
            GetMock<IUserRepository>().Setup(o => o.Add(It.IsAny<User>())).Callback((User u) => user = u);

            var request = new AddUserRequest(ValidUserName, ValidDisplayName, ValidEmail);
            Execute(request);

            Assert.AreEqual(0, user.Id);
            Assert.AreEqual(ValidUserName, user.UserName);
            Assert.AreEqual(ValidDisplayName, user.DisplayName);
            Assert.AreEqual("", user.RealName);
            Assert.AreEqual(ValidEmail, user.Email);
            Assert.AreEqual(Role.Player, user.GlobalRole);
            Assert.AreEqual(expectedEncryptedPassword, user.EncryptedPassword);
            Assert.AreEqual(expectedSalt, user.Salt);
        }

        [Test]
        public void AddUser_WithValidInput_SendsRegistrationEmail()
        {
            const string subject = "Poker Bunch Registration";
            const string body = @"Thanks for registering with Poker Bunch.

Here is your password:
aaaaaaaa

Please sign in here: http://pokerbunch.com/-/auth/login";

            SetupPasswordCharacters();
            SetupSaltCharacters();
            string email = null;
            IMessage message = null;
            GetMock<IMessageSender>()
                .Setup(o => o.Send(It.IsAny<string>(), It.IsAny<IMessage>()))
                .Callback((string e, IMessage m) =>
                {
                    email = e;
                    message = m;
                });

            var request = new AddUserRequest(ValidUserName, ValidDisplayName, ValidEmail);
            Execute(request);

            Assert.AreEqual(ValidEmail, email);
            Assert.AreEqual(subject, message.Subject);
            Assert.AreEqual(body, message.Body);
        }

        private void SetupPasswordCharacters()
        {
            GetMock<IRandomService>().Setup(o => o.GetPasswordCharacters()).Returns("a");
        }

        private void SetupSaltCharacters()
        {
            GetMock<IRandomService>().Setup(o => o.GetSaltCharacters()).Returns("b");
        }


        private AddUserResult Execute(AddUserRequest request)
        {
            return AddUserInteractor.Execute(
                GetMock<IUserRepository>().Object,
                GetMock<IRandomService>().Object,
                GetMock<IMessageSender>().Object,
                request);
        }
    }
}
