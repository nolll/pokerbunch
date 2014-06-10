using System;
using System.Web.Mvc;
using NUnit.Framework;
using Tests.Common;
using Web.Commands.UserCommands;
using Web.Controllers;
using Web.ModelFactories.UserModelFactories;
using Web.ModelServices;
using Web.Models.UserModels.List;

namespace Tests.Web.ControllerTests
{
    class UserControllerTests : MockContainer
    {
        private UserController _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new UserController(
                GetMock<IUserCommandProvider>().Object,
                GetMock<IUserModelService>().Object,
                GetMock<IUserListPageBuilder>().Object);
        }

        [Test]
        public void List_RequiresAdmin()
        {
            Func<ActionResult> methodToTest = _sut.List;
            var result = SecurityTestHelper.RequiresAdmin(methodToTest);

            Assert.IsTrue(result);
        }

        [Test]
        public void List_GetsPageModelFromModelBuilder()
        {
            var model = new UserListPageModel();

            GetMock<IUserListPageBuilder>().Setup(o => o.Build()).Returns(model);

            var result = _sut.List() as ViewResult;

            Assert.NotNull(result);
            Assert.AreEqual(model, result.Model);
        }
    }
}
