using System;
using System.Web.Mvc;
using Application.UseCases.AppContext;
using Application.UseCases.BunchList;
using NUnit.Framework;
using Tests.Common;
using Web.Commands.HomegameCommands;
using Web.Controllers;
using Web.ModelFactories.HomegameModelFactories;

namespace Tests.Web.ControllerTests
{
    class HomegameControllerTests : MockContainer
    {
        private HomegameController _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new HomegameController(
                GetMock<IAppContextInteractor>().Object,
                GetMock<IBunchListInteractor>().Object,
                GetMock<IHomegameCommandProvider>().Object,
                GetMock<IHomegameDetailsPageBuilder>().Object,
                GetMock<IAddHomegamePageBuilder>().Object,
                GetMock<IAddHomegameConfirmationPageBuilder>().Object,
                GetMock<IEditHomegamePageBuilder>().Object,
                GetMock<IJoinHomegamePageBuilder>().Object,
                GetMock<IJoinHomegameConfirmationPageBuilder>().Object);
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
