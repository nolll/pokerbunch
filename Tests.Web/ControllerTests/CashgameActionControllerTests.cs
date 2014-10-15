using System;
using System.Web.Mvc;
using NUnit.Framework;
using Tests.Common;
using Web.Controllers;

namespace Tests.Web.ControllerTests
{
    public class CashgameActionControllerTests : TestBase
    {
        [Test]
        public void Action_RequiresPlayer()
        {
            var sut = GetSut();
            Func<string, string, int, ActionResult> methodToTest = sut.Action;
            var result = SecurityTestHelper.RequiresPlayer(methodToTest);

            Assert.IsTrue(result);
        }

        private CashgameActionController GetSut()
        {
            return new CashgameActionController();
        }
    }
}