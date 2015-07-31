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

            Assert.AreEqual(TestData.UserNameA, result.UserName);
        }

        [Test]
        public void EditUserForm_RealNameIsSet()
        {
            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual(TestData.UserRealNameA, result.RealName);
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

        private EditUserFormRequest CreateRequest()
        {
            return new EditUserFormRequest(TestData.UserNameA);
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
