using Core.UseCases.EditUser;
using Core.UseCases.EditUserForm;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class EditUserFormTests : TestBase
    {
        [Test]
        public void EditUserForm_UserNameIsSet()
        {
            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual(Constants.UserNameA, result.UserName);
        }

        [Test]
        public void EditUserForm_RealNameIsSet()
        {
            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual(Constants.UserRealNameA, result.RealName);
        }

        [Test]
        public void EditUserForm_DisplayNameIsSet()
        {
            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual(Constants.UserDisplayNameA, result.DisplayName);
        }

        [Test]
        public void EditUserForm_EmailIsSet()
        {
            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual(Constants.UserEmailA, result.Email);
        }

        private EditUserFormRequest CreateRequest()
        {
            return new EditUserFormRequest(Constants.UserNameA);
        }

        private EditUserFormInteractor Sut
        {
            get
            {
                return new EditUserFormInteractor(Repos.User);
            }
        }
    }
}
