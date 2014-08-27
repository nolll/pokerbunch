using System;
using System.Web.Mvc;
using Application.Urls;
using Application.UseCases.BunchContext;
using Application.UseCases.PlayerBadges;
using Application.UseCases.PlayerDetails;
using Application.UseCases.PlayerFacts;
using Application.UseCases.PlayerList;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeCommands;
using Web.Commands.PlayerCommands;
using Web.Controllers;
using Web.ModelFactories.PlayerModelFactories;

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
            var listUrl = new PlayerIndexUrl(slug);

            GetMock<IPlayerCommandProvider>().Setup(o => o.GetDeleteCommand(slug, playerId)).Returns(new SuccessfulCommandInTest());

            var sut = GetSut();
            var result = sut.Delete(slug, playerId) as RedirectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(listUrl.Relative, result.Url);
        }

        [Test]
        public void Delete_WithFailedCommand_RedirectsToPlayerList()
        {
            const string slug = "a";
            const int playerId = 1;
            var playerDetailsUrl = new PlayerDetailsUrl(slug, playerId);

            GetMock<IPlayerCommandProvider>().Setup(o => o.GetDeleteCommand(slug, playerId)).Returns(new FailedCommandInTest());

            var sut = GetSut();
            var result = sut.Delete(slug, playerId) as RedirectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(playerDetailsUrl.Relative, result.Url);
        }

        private PlayerController GetSut()
        {
			return new PlayerController(
                GetMock<IBunchContextInteractor>().Object,
                GetMock<IPlayerDetailsInteractor>().Object,
                GetMock<IPlayerFactsInteractor>().Object,
                GetMock<IPlayerBadgesInteractor>().Object,
                GetMock<IPlayerListInteractor>().Object,
                GetMock<IPlayerCommandProvider>().Object,
                GetMock<IAddPlayerPageBuilder>().Object,
                GetMock<IInvitePlayerPageBuilder>().Object);
		}
	}
}