using System;
using System.Web.Mvc;
using NUnit.Framework;
using Tests.Common;
using Web.Commands.HomegameCommands;
using Web.Controllers;

namespace Tests.Web.ControllerTests
{
    class HomegameControllerTests : TestBase
    {
        [Test]
        public void List_RequiresAdmin()
        {
            Func<ActionResult> methodToTest = Sut.List;
            var result = SecurityTestHelper.RequiresAdmin(methodToTest);

            Assert.IsTrue(result);
        }

        private HomegameController Sut
        {
            get
            {
                return new HomegameController(
                    GetMock<IBunchCommandProvider>().Object);
            }
        }
    }
}
