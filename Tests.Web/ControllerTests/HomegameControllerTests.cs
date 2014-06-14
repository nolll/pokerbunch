using System;
using System.Web.Mvc;
using NUnit.Framework;
using Tests.Common;
using Web.Commands.HomegameCommands;
using Web.Controllers;
using Web.ModelFactories.HomegameModelFactories;
using Web.ModelServices;

namespace Tests.Web.ControllerTests
{
    class HomegameControllerTests : MockContainer
    {
        private HomegameController _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new HomegameController(
                GetMock<IHomegameCommandProvider>().Object,
                GetMock<IHomegameModelService>().Object,
                GetMock<IBunchListPageBuilder>().Object,
                GetMock<IHomegameDetailsPageBuilder>().Object);
        }

        [Test]
        public void List_RequiresAdmin()
        {
            Func<ActionResult> methodToTest = _sut.List;
            var result = SecurityTestHelper.RequiresAdmin(methodToTest);

            Assert.IsTrue(result);
        }

        [Test]
        [Ignore("Do we really need tests like this?")]
        public void List_GetsPageModelFromModelBuilder()
        {
            //var model = new BunchListPageModel();

            //GetMock<IBunchListPageBuilder>().Setup(o => o.Build()).Returns(model);

            //var result = _sut.List() as ViewResult;

            //Assert.NotNull(result);
            //Assert.AreEqual(model, result.Model);
        }
    }
}
