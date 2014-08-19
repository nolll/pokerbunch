using System;
using System.Web.Mvc;
using Application.UseCases.AppContext;
using Application.UseCases.UserDetails;
using Application.UseCases.UserList;
using NUnit.Framework;
using Tests.Common;
using Web.Commands.UserCommands;
using Web.Controllers;
using Web.ModelFactories.UserModelFactories;

namespace Tests.Web.ControllerTests
{
    class UserControllerTests : MockContainer
    {
        private UserController _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new UserController(
                GetMock<IAppContextInteractor>().Object,
                GetMock<IUserDetailsInteractor>().Object,
                GetMock<IUserListInteractor>().Object,
                GetMock<IUserCommandProvider>().Object,
                GetMock<IAddUserPageBuilder>().Object,
                GetMock<IAddUserConfirmationPageBuilder>().Object,
                GetMock<IEditUserPageBuilder>().Object,
                GetMock<IChangePasswordPageBuilder>().Object,
                GetMock<IForgotPasswordPageBuilder>().Object);
        }

        [Test]
        public void List_RequiresAdmin()
        {
            Func<ActionResult> methodToTest = _sut.List;
            var result = SecurityTestHelper.RequiresAdmin(methodToTest);

            Assert.IsTrue(result);
        }
    }
}