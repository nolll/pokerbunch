using System.Linq;
using Application.Exceptions;
using Application.Urls;
using Application.UseCases.AddUser;
using Core.Repositories;
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

        private AddUserResult Execute(AddUserRequest request)
        {
            return AddUserInteractor.Execute(
                GetMock<IUserRepository>().Object,
                request);
        }
    }
}
