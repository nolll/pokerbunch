using System;
using System.Web.Mvc;
using NUnit.Framework;
using Tests.Common;
using Web.Controllers;

namespace Tests.Web.ControllerTests
{
	public class PlayerControllerTests : TestBase
    {
        [Test]
        public void Details_RequiresPlayer()
        {
            Func<string, int, ActionResult> methodToTest = new PlayerDetailsController().Details;
            var result = SecurityTestHelper.RequiresPlayer(methodToTest);

            Assert.IsTrue(result);
        }

        [Test]
		public void Delete_RequiresManager()
        {
            Func<string, int, ActionResult> methodToTest = new DeletePlayerController().Delete;
            var result = SecurityTestHelper.RequiresManager(methodToTest);
            
            Assert.IsTrue(result);
        }
	}
}