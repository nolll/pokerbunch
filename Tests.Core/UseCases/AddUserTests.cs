using System.Linq;
using Core.Entities;
using Core.Exceptions;
using Core.Urls;
using Core.UseCases.AddUser;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class AddUserTests : TestBase
    {
        private const string ValidUserName = "a";
        private const string ValidDisplayName = "b";
        private const string ValidEmail = "a@b.com";
        private readonly string _existingUserName = TestData.UserA.UserName;
        private readonly string _existingEmail = TestData.UserA.Email;

        [Test]
        public void AddUser_ReturnUrlIsSet()
        {
            var request = new AddUserRequest(ValidUserName, ValidDisplayName, ValidEmail);

            var result = Sut.Execute(request);

            Assert.IsInstanceOf<AddUserConfirmationUrl>(result.ReturnUrl);
        }

        [Test]
        public void AddUser_WithEmptyUserName_ThrowsValidationError()
        {
            var request = new AddUserRequest("", ValidDisplayName, ValidEmail);

            var ex = Assert.Throws<ValidationException>(() => Sut.Execute(request));
            Assert.AreEqual(1, ex.Messages.Count());
        }

        [Test]
        public void AddUser_WithEmptyDisplayName_ThrowsValidationError()
        {
            var request = new AddUserRequest(ValidUserName, "", ValidEmail);

            var ex = Assert.Throws<ValidationException>(() => Sut.Execute(request));
            Assert.AreEqual(1, ex.Messages.Count());
        }

        [Test]
        public void AddUser_WithEmptyEmail_ThrowsValidationError()
        {
            var request = new AddUserRequest(ValidUserName, ValidDisplayName, "");

            var ex = Assert.Throws<ValidationException>(() => Sut.Execute(request));
            Assert.AreEqual(1, ex.Messages.Count());
        }

        [Test]
        public void AddUser_UserNameAlreadyInUse_ThrowsException()
        {
            var request = new AddUserRequest(_existingUserName, ValidDisplayName, ValidEmail);

            Assert.Throws<UserExistsException>(() => Sut.Execute(request));
        }

        [Test]
        public void AddUser_EmailAlreadyInUse_ThrowsException()
        {
            var request = new AddUserRequest(ValidUserName, ValidDisplayName, _existingEmail);

            Assert.Throws<EmailExistsException>(() => Sut.Execute(request));
        }

        [Test]
        public void AddUser_WithValidInput_UserWithCorrectPropertiesIsAdded()
        {
            const string expectedEncryptedPassword = "0478095c8ece0bbc11f94663ac2c4f10b29666de";
            const string expectedSalt = "aaaaaaaaaa";

            var request = new AddUserRequest(ValidUserName, ValidDisplayName, ValidEmail);
            Sut.Execute(request);

            var user = Repos.User.Added;

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

            var request = new AddUserRequest(ValidUserName, ValidDisplayName, ValidEmail);
            Sut.Execute(request);

            Assert.AreEqual(ValidEmail, Services.MessageSender.To);
            Assert.AreEqual(subject, Services.MessageSender.Message.Subject);
            Assert.AreEqual(body, Services.MessageSender.Message.Body);
        }

        private AddUserInteractor Sut
        {
            get
            {
                return new AddUserInteractor(
                    Repos.User,
                    Services.RandomService,
                    Services.MessageSender);
            }
        }
    }
}
