using Application.UseCases.EditUserForm;
using Core.Repositories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class EditUserFormTests : TestBase
    {
        private const string UserName = "a";
        private const string RealName = "b";
        private const string DisplayName = "c";
        private const string Email = "d";

        [Test]
        public void EditUserForm_UserNameIsSet()
        {
            SetupUser();

            var result = Execute(CreateRequest());

            Assert.AreEqual(UserName, result.UserName);
        }

        [Test]
        public void EditUserForm_RealNameIsSet()
        {
            SetupUser();

            var result = Execute(CreateRequest());

            Assert.AreEqual(RealName, result.RealName);
        }

        [Test]
        public void EditUserForm_DisplayNameIsSet()
        {
            SetupUser();

            var result = Execute(CreateRequest());

            Assert.AreEqual(DisplayName, result.DisplayName);
        }

        [Test]
        public void EditUserForm_EmailIsSet()
        {
            SetupUser();

            var result = Execute(CreateRequest());

            Assert.AreEqual(Email, result.Email);
        }

        private void SetupUser()
        {
            var user = new UserInTest(userName: UserName, realName: RealName, displayName: DisplayName, email: Email);
            GetMock<IUserRepository>().Setup(o => o.GetByNameOrEmail(UserName)).Returns(user);
        }

        private EditUserFormRequest CreateRequest()
        {
            return new EditUserFormRequest(UserName);
        }

        private EditUserFormResult Execute(EditUserFormRequest request)
        {
            return EditUserFormInteractor.Execute(GetMock<IUserRepository>().Object, request);
        }
    }
}
