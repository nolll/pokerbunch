using System.Collections.Generic;
using System.Web.Mvc;
using Core.Classes;
using Core.Exceptions;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.Controllers;

namespace Web.Tests.ControllerTests{

	public class CashgameControllerTests : WebMockContainer
	{
        private const string Slug = "homegame1";
        private const string DateStr = "2010-01-01";
        private const string PlayerName = "Player 1";
        private const string UserName = "user1";

        [Test]
		public void Matrix_NotAuthorized_ThrowsException(){
            Mocks.HomegameRepositoryMock.Setup(o => o.GetByName(Slug)).Returns(new FakeHomegame());
            Mocks.UserContextMock.Setup(o => o.RequirePlayer(It.IsAny<Homegame>())).Throws<AccessDeniedException>();

            var sut = GetSut();

            Assert.Throws<AccessDeniedException>(() => sut.Matrix(Slug));
		}

		[Test]
		public void Matrix_Authorized_ShowsCorrectView(){
            Mocks.HomegameRepositoryMock.Setup(o => o.GetByName(Slug)).Returns(new FakeHomegame());
            Mocks.UserContextMock.Setup(o => o.GetUser()).Returns(new FakeUser());

		    var sut = GetSut();
            var viewResult = (ViewResult)sut.Matrix(Slug);

			Assert.AreEqual("Matrix/MatrixPage", viewResult.ViewName);
		}

        [Test]
		public void Leaderboard_NotAuthorized_ThrowsException(){
            Mocks.HomegameRepositoryMock.Setup(o => o.GetByName(Slug)).Returns(new FakeHomegame());
            Mocks.UserContextMock.Setup(o => o.RequirePlayer(It.IsAny<Homegame>())).Throws<AccessDeniedException>();

            var sut = GetSut();

            Assert.Throws<AccessDeniedException>(() => sut.Leaderboard(Slug));
		}

        [Test]
		public void Leaderboard_Authorized_ShowsCorrectView(){
            Mocks.HomegameRepositoryMock.Setup(o => o.GetByName(Slug)).Returns(new FakeHomegame());
            Mocks.CashgameRepositoryMock.Setup(o => o.GetSuite(It.IsAny<Homegame>(), It.IsAny<int?>())).Returns(new CashgameSuite());
            Mocks.UserContextMock.Setup(o => o.GetUser()).Returns(new FakeUser());

		    var sut = GetSut();
            var viewResult = (ViewResult)sut.Leaderboard(Slug);

			Assert.AreEqual("Leaderboard/LeaderboardPage", viewResult.ViewName);
		}

        [Test]
		public void Details_NotAuthorized_ThrowsException(){
            Mocks.HomegameRepositoryMock.Setup(o => o.GetByName(Slug)).Returns(new FakeHomegame());
            Mocks.UserContextMock.Setup(o => o.RequirePlayer(It.IsAny<Homegame>())).Throws<AccessDeniedException>();

            var sut = GetSut();

            Assert.Throws<AccessDeniedException>(() => sut.Details(Slug, DateStr));
		}

		[Test]
		public void Details_ReturnsCorrectView(){
            Mocks.HomegameRepositoryMock.Setup(o => o.GetByName(Slug)).Returns(new FakeHomegame());
            Mocks.UserContextMock.Setup(o => o.GetUser()).Returns(new FakeUser());
            Mocks.CashgameRepositoryMock.Setup(o => o.GetByDateString(It.IsAny<Homegame>(), It.IsAny<string>())).Returns(new FakeCashgame());
            Mocks.PlayerRepositoryMock.Setup(o => o.GetByUserName(It.IsAny<Homegame>(), It.IsAny<string>())).Returns(new FakePlayer());
            
            var sut = GetSut();
            var viewResult = (ViewResult)sut.Details(Slug, DateStr);

            Assert.AreEqual("Details/DetailsPage", viewResult.ViewName);
		}

        [Test]
		public void ActionAction_NotAuthorized_ThrowsException(){
            Mocks.HomegameRepositoryMock.Setup(o => o.GetByName(Slug)).Returns(new FakeHomegame());
            Mocks.UserContextMock.Setup(o => o.RequirePlayer(It.IsAny<Homegame>())).Throws<AccessDeniedException>();

            var sut = GetSut();

            Assert.Throws<AccessDeniedException>(() => sut.Action(Slug, DateStr, PlayerName));
		}

		[Test]
		public void ActionAction_ReturnsCorrectModel()
		{
		    var homegame = new FakeHomegame();
		    var user = new FakeUser();
            var player = new FakePlayer(id: 1);
		    var cashgameResult = new FakeCashgameResult(player);
		    var cashgame = new FakeCashgame(results: new List<CashgameResult> {cashgameResult});
            Mocks.HomegameRepositoryMock.Setup(o => o.GetByName(Slug)).Returns(homegame);
            Mocks.UserContextMock.Setup(o => o.GetUser()).Returns(user);
            Mocks.CashgameRepositoryMock.Setup(o => o.GetByDateString(homegame, DateStr)).Returns(cashgame);
            Mocks.PlayerRepositoryMock.Setup(o => o.GetByName(homegame, PlayerName)).Returns(player);

			var sut = GetSut();
            var viewResult = (ViewResult)sut.Action(Slug, DateStr, PlayerName);

            Assert.AreEqual("Action/Action", viewResult.ViewName);
		}

        [Test]
		public void ActionBuyin_NotAuthorized_ThrowsException(){
            Mocks.HomegameRepositoryMock.Setup(o => o.GetByName(Slug)).Returns(new FakeHomegame());
            Mocks.UserContextMock.Setup(o => o.RequirePlayer(It.IsAny<Homegame>())).Throws<AccessDeniedException>();

            var sut = GetSut();

            Assert.Throws<AccessDeniedException>(() => sut.Buyin(Slug, PlayerName));
		}

		[Test]
		public void ActionBuyin_WithPlayerRightsAndIsAnotherPlayer_ThrowsException()
		{
		    const int firstUserId = 1;
		    const int secondUserId = 2;
			var homegame = new FakeHomegame();
			var cashgame = new FakeCashgame();
		    var user = new FakeUser(id: firstUserId);
			var player = new FakePlayer(userId: secondUserId);

            Mocks.HomegameRepositoryMock.Setup(o => o.GetByName(Slug)).Returns(homegame);
            Mocks.CashgameRepositoryMock.Setup(o => o.GetRunning(homegame)).Returns(cashgame);
            Mocks.UserContextMock.Setup(o => o.GetUser()).Returns(user);
            Mocks.PlayerRepositoryMock.Setup(o => o.GetByName(homegame, PlayerName)).Returns(player);

            var sut = GetSut();

            Assert.Throws<AccessDeniedException>(() => sut.Buyin(Slug, PlayerName));
		}

        //todo: finish these tests before continuing with more controller tests
		/*
        [Test]
		public void ActionBuyin_ReturnsModel(){
			homegameRepositoryMock.returns('getByName', new Homegame());
            TestHelper::setupUserWithPlayerRights(userContext);
            $cashgame = new Cashgame();
			cashgameRepositoryMock.returns('getRunning', $cashgame);
			$player = new Player();
			$player.setUserName('user1');
			playerRepositoryMock.returns('getByName', $player);

			$viewResult = sut.action_buyin("homegame1", "Player 1");

			assertIsA($viewResult.model, 'app\Cashgame\Action\BuyinModel');
		}

		[Test]
		public void ActionBuyinPost_NotAuthorized_ThrowsException(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			userContext.throwOn('requirePlayer');
			expectException();

			sut.action_buyin_post("homegame1", "Player 1");
		}

		[Test]
		public void ActionBuyinPost_PlayerIsNotInGame_AddsCheckpoint(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights(userContext);
			$cashgame = new Cashgame();
			cashgameRepositoryMock.returns('getRunning', $cashgame);
			$player = new Player();
			playerRepositoryMock.returns('getByName', $player);
			setupValidBuyinValidator();

			cashgameRepositoryMock.expectOnce("addCheckpoint");

			sut.action_buyin_post("homegame1", "Player 1");
		}

		[Test]
		public void ActionBuyinPost_GameIsNotStarted_StartsGame(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights(userContext);
			$cashgame = new Cashgame();
			cashgameRepositoryMock.returns('getRunning', $cashgame);
			$player = new Player();
			playerRepositoryMock.returns('getByName', $player);
			setupValidBuyinValidator();

			cashgameRepositoryMock.expectOnce("startGame");

			sut.action_buyin_post("homegame1", "Player 1");
		}

		[Test]
		public void ActionBuyinPost_GameIsStarted_DoesNotStartGame(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights(userContext);
			$cashgame = new Cashgame();
			$cashgame.setIsStarted(true);
			cashgameRepositoryMock.returns('getRunning', $cashgame);
			$player = new Player();
			playerRepositoryMock.returns('getByName', $player);
			setupValidBuyinValidator();

			cashgameRepositoryMock.expectNever("startGame");

			sut.action_buyin_post("homegame1", "Player 1");
		}

		[Test]
		public void ActionBuyinPost_RedirectsToRunningCashgame(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights(userContext);
			$cashgame = new Cashgame();
			cashgameRepositoryMock.returns('getRunning', $cashgame);
			$player = new Player();
			playerRepositoryMock.returns('getByName', $player);
			setupValidBuyinValidator();

			$urlModel = sut.action_buyin_post("homegame1", "Player 1");

			assertIsA($urlModel, 'app\Urls\RunningCashgameUrlModel');
		}

		[Test]
		public void ActionBuyinPost_WithInvalidForm_ReturnsModel(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights(userContext);
			$cashgame = new Cashgame();
			$cashgame.setIsStarted(true);
			cashgameRepositoryMock.returns('getRunning', $cashgame);
			$player = new Player();
			$player.setUserName('user1');
			playerRepositoryMock.returns('getByName', $player);
			setupInvalidBuyinValidator();

			$viewResult = sut.action_buyin_post("homegame1", "Player 1");

			assertIsA($viewResult.model, 'app\Cashgame\Action\BuyinModel');
		}
        */

        private CashgameController GetSut()
        {
            return new CashgameController(
                Mocks.HomegameRepositoryMock.Object,
                Mocks.UserContextMock.Object,
                Mocks.CashgameRepositoryMock.Object,
                Mocks.PlayerRepositoryMock.Object,
                Mocks.MatrixPageModelFactoryMock.Object,
                Mocks.CashgameFactoryMock.Object,
                Mocks.BuyinPageModelFactoryMock.Object,
                Mocks.ReportPageModelFactoryMock.Object,
                Mocks.CashoutPageModelFactoryMock.Object,
                Mocks.EndPageModelFactoryMock.Object,
                Mocks.ActionPageModelFactoryMock.Object,
                Mocks.AddCashgamePageModelFactoryMock.Object,
                Mocks.CashgameChartPageModelFactoryMock.Object,
                Mocks.CashgameDetailsPageModelFactoryMock.Object,
                Mocks.CashgameEditPageModelFactoryMock.Object,
                Mocks.CashgameFactsPageModelFactoryMock.Object,
                Mocks.CashgameLeaderboardPageModelFactoryMock.Object,
                Mocks.CashgameListingPageModelFactoryMock.Object,
                Mocks.RunningCashgamePageModelFactoryMock.Object,
                Mocks.CashgameModelMapperMock.Object,
                Mocks.CheckpointModelMapperMock.Object,
                Mocks.UrlProviderMock.Object,
                Mocks.CashgameSuiteChartModelFactoryMock.Object,
                Mocks.ActionChartModelFactoryMock.Object,
                Mocks.CashgameDetailsChartModelFactoryMock.Object,
                Mocks.TimeProviderMock.Object,
                Mocks.CheckpointRepositoryMock.Object);
        }

	}

}