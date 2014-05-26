using System;
using System.Web.Mvc;
using Application.Services;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeCommands;
using Web.Commands.PlayerCommands;
using Web.Controllers;
using Web.ModelFactories.PlayerModelFactories;
using Web.ModelServices;

namespace Tests.Web.ControllerTests
{
	public class PlayerControllerTests : MockContainer
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
        
        [Test]
		public void Delete_WithSuccessfulCommand_RedirectsToPlayerList()
        {
            const string slug = "a";
		    const int playerId = 1;
            const string listUrl = "c";

            GetMock<IPlayerCommandProvider>().Setup(o => o.GetDeleteCommand(slug, playerId)).Returns(new FakeSuccessfulCommand());
            GetMock<IUrlProvider>().Setup(o => o.GetPlayerIndexUrl(slug)).Returns(listUrl);

            var sut = GetSut();
            var result = sut.Delete(slug, playerId) as RedirectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(listUrl, result.Url);
        }

        [Test]
        public void Delete_WithFailedCommand_RedirectsToPlayerList()
        {
            const string slug = "a";
            const int playerId = 1;
            const string playerUrl = "c";

            GetMock<IPlayerCommandProvider>().Setup(o => o.GetDeleteCommand(slug, playerId)).Returns(new FakeFailedCommand());
            GetMock<IUrlProvider>().Setup(o => o.GetPlayerDetailsUrl(slug, playerId)).Returns(playerUrl);

            var sut = GetSut();
            var result = sut.Delete(slug, playerId) as RedirectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(playerUrl, result.Url);
        }

        private PlayerController GetSut()
        {
			return new PlayerController(
                GetMock<IPlayerModelService>().Object,
                GetMock<IUrlProvider>().Object,
                GetMock<IPlayerCommandProvider>().Object,
                GetMock<IPlayerListPageBuilder>().Object);
		}
	}
}