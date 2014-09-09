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
            var sut = GetSut();
            Func<string, int, ActionResult> methodToTest = sut.Details;
            var result = SecurityTestHelper.RequiresPlayer(methodToTest);

            Assert.IsTrue(result);
        }

        [Test]
		public void Delete_RequiresManager()
        {
			var sut = GetSut();
            Func<string, int, ActionResult> methodToTest = sut.Delete;
            var result = SecurityTestHelper.RequiresManager(methodToTest);
            
            Assert.IsTrue(result);
        }
        
        private PlayerController GetSut()
        {
			return new PlayerController();
		}
	}
}