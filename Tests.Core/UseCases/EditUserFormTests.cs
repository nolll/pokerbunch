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

            Assert.AreEqual(TestData.UserA.UserName, result.UserName);
        }

        [Test]
        public void EditUserForm_RealNameIsSet()
        {
            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual(TestData.UserA.RealName, result.RealName);
        }

        [Test]
        public void EditUserForm_DisplayNameIsSet()
        {
            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual(TestData.UserDisplayNameA, result.DisplayName);
        }

        [Test]
        public void EditUserForm_EmailIsSet()
        {
            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual(TestData.UserEmailA, result.Email);
        }

        private EditUserFormInteractor.EditUserFormRequest CreateRequest()
        {
            return new EditUserFormInteractor.EditUserFormRequest(TestData.UserA.UserName);
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
