using System;
using System.Web.Mvc;
using NUnit.Framework;
using Tests.Common;
using Web.Controllers;

namespace Tests.Web.ControllerTests
{
    class UserListControllerTests : TestBase
    {
        [Test]
        public void List_RequiresAdmin()
        {
            Func<ActionResult> methodToTest = Sut.List;
            var result = SecurityTestHelper.RequiresAdmin(methodToTest);

            Assert.IsTrue(result);
        }

        private UserListController Sut
        {
            get
            {
                return new UserListController();
            }
        }
    }
}