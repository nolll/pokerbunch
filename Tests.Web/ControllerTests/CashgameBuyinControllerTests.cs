using System;
using System.Web.Mvc;
using NUnit.Framework;
using Tests.Common;
using Web.Controllers;

namespace Tests.Web.ControllerTests
{
    public class CashgameBuyinControllerTests : TestBase
    {
        [Test]
        public void Buyin_RequiresOwnPlayer()
        {
            var sut = GetSut();
            Func<string, int, ActionResult> methodToTest = new CashgameBuyinController().Buyin;
            var result = SecurityTestHelper.RequiresOwnPlayer(methodToTest);

            Assert.IsTrue(result);
        }

        private CashgameBuyinController GetSut()
        {
            return new CashgameBuyinController();
        }
    }
}