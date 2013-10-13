using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Core.Classes;
using Core.Exceptions;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Web.Controllers;

namespace Web.Tests.ControllerTests{

	public class CashgameControllerTests : MockContainer
	{
        private const string Slug = "homegame1";
        private const string DateStr = "2010-01-01";
        private const string PlayerName = "Player 1";
        private const string UserName = "user1";

        [Test]
		public void Matrix_NotAuthorized_ThrowsException(){
            WebMocks.HomegameRepositoryMock.Setup(o => o.GetByName(Slug)).Returns(new Homegame());
            WebMocks.UserContextMock.Setup(o => o.RequirePlayer(It.IsAny<Homegame>())).Throws<AccessDeniedException>();

            var sut = GetSut();

            Assert.Throws<AccessDeniedException>(() => sut.Matrix(Slug));
		}

		[Test]
		public void Matrix_Authorized_ShowsCorrectView(){
            WebMocks.HomegameRepositoryMock.Setup(o => o.GetByName(Slug)).Returns(new Homegame());
            WebMocks.UserContextMock.Setup(o => o.GetUser()).Returns(new User());

		    var sut = GetSut();
            var viewResult = (ViewResult)sut.Matrix(Slug);

			Assert.AreEqual("Matrix/MatrixPage", viewResult.ViewName);
		}

        [Test]
		public void Leaderboard_NotAuthorized_ThrowsException(){
            WebMocks.HomegameRepositoryMock.Setup(o => o.GetByName(Slug)).Returns(new Homegame());
            WebMocks.UserContextMock.Setup(o => o.RequirePlayer(It.IsAny<Homegame>())).Throws<AccessDeniedException>();

            var sut = GetSut();

            Assert.Throws<AccessDeniedException>(() => sut.Leaderboard(Slug));
		}

        [Test]
		public void Leaderboard_Authorized_ShowsCorrectView(){
            WebMocks.HomegameRepositoryMock.Setup(o => o.GetByName(Slug)).Returns(new Homegame());
            WebMocks.CashgameRepositoryMock.Setup(o => o.GetSuite(It.IsAny<Homegame>(), It.IsAny<int?>())).Returns(new CashgameSuite());
            WebMocks.UserContextMock.Setup(o => o.GetUser()).Returns(new User());

		    var sut = GetSut();
            var viewResult = (ViewResult)sut.Leaderboard(Slug);

			Assert.AreEqual("Leaderboard/LeaderboardPage", viewResult.ViewName);
		}

        [Test]
		public void Details_NotAuthorized_ThrowsException(){
            WebMocks.HomegameRepositoryMock.Setup(o => o.GetByName(Slug)).Returns(new Homegame());
            WebMocks.UserContextMock.Setup(o => o.RequirePlayer(It.IsAny<Homegame>())).Throws<AccessDeniedException>();

            var sut = GetSut();

            Assert.Throws<AccessDeniedException>(() => sut.Details(Slug, DateStr));
		}

		[Test]
		public void Details_ReturnsCorrectView(){
            WebMocks.HomegameRepositoryMock.Setup(o => o.GetByName(Slug)).Returns(new Homegame());
            WebMocks.UserContextMock.Setup(o => o.GetUser()).Returns(new User());
            WebMocks.CashgameRepositoryMock.Setup(o => o.GetByDate(It.IsAny<Homegame>(), It.IsAny<DateTime>())).Returns(new Cashgame());
            WebMocks.PlayerRepositoryMock.Setup(o => o.GetByUserName(It.IsAny<Homegame>(), It.IsAny<string>())).Returns(new Player());
            
            var sut = GetSut();
            var viewResult = (ViewResult)sut.Details(Slug, DateStr);

            Assert.AreEqual("Details/DetailsPage", viewResult.ViewName);
		}

        [Test]
		public void ActionAction_NotAuthorized_ThrowsException(){
            WebMocks.HomegameRepositoryMock.Setup(o => o.GetByName(Slug)).Returns(new Homegame());
            WebMocks.UserContextMock.Setup(o => o.RequirePlayer(It.IsAny<Homegame>())).Throws<AccessDeniedException>();

            var sut = GetSut();

            Assert.Throws<AccessDeniedException>(() => sut.Action(Slug, DateStr, PlayerName));
		}

		[Test]
		public void ActionAction_ReturnsCorrectModel()
		{
		    var homegame = new Homegame();
		    var user = new User();
            var player = new Player {UserName = UserName, Id = 1};
		    var cashgameResult = new CashgameResult {Player = player};
		    var cashgame = new Cashgame {Results = new List<CashgameResult> {cashgameResult}};
            WebMocks.HomegameRepositoryMock.Setup(o => o.GetByName(Slug)).Returns(homegame);
            WebMocks.UserContextMock.Setup(o => o.GetUser()).Returns(user);
            WebMocks.CashgameRepositoryMock.Setup(o => o.GetByDateString(homegame, DateStr)).Returns(cashgame);
            WebMocks.PlayerRepositoryMock.Setup(o => o.GetByName(homegame, PlayerName)).Returns(player);

			var sut = GetSut();
            var viewResult = (ViewResult)sut.Action(Slug, DateStr, PlayerName);

            Assert.AreEqual("Action/Action", viewResult.ViewName);
		}

        [Test]
		public void ActionBuyin_NotAuthorized_ThrowsException(){
            WebMocks.HomegameRepositoryMock.Setup(o => o.GetByName(Slug)).Returns(new Homegame());
            WebMocks.UserContextMock.Setup(o => o.RequirePlayer(It.IsAny<Homegame>())).Throws<AccessDeniedException>();

            var sut = GetSut();

            Assert.Throws<AccessDeniedException>(() => sut.Buyin(Slug, PlayerName));
		}

		[Test]
		public void ActionBuyin_WithPlayerRightsAndIsAnotherPlayer_ThrowsException(){
			var homegame = new Homegame();
			var cashgame = new Cashgame();
		    var user = new User();
			var player = new Player {UserName = "otherUser"};

            WebMocks.HomegameRepositoryMock.Setup(o => o.GetByName(Slug)).Returns(homegame);
            WebMocks.CashgameRepositoryMock.Setup(o => o.GetRunning(homegame)).Returns(cashgame);
            WebMocks.UserContextMock.Setup(o => o.GetUser()).Returns(user);
            WebMocks.PlayerRepositoryMock.Setup(o => o.GetByName(homegame, PlayerName)).Returns(player);

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
                WebMocks.HomegameRepositoryMock.Object,
                WebMocks.UserContextMock.Object,
                WebMocks.CashgameRepositoryMock.Object,
                WebMocks.PlayerRepositoryMock.Object,
                WebMocks.MatrixPageModelFactoryMock.Object,
                WebMocks.CashgameFactoryMock.Object,
                WebMocks.TimeProviderMock.Object,
                WebMocks.BuyinPageModelFactoryMock.Object,
                WebMocks.ReportPageModelFactoryMock.Object,
                WebMocks.CashoutPageModelFactoryMock.Object,
                WebMocks.EndPageModelFactoryMock.Object,
                WebMocks.ActionPageModelFactoryMock.Object,
                WebMocks.AddCashgamePageModelFactoryMock.Object,
                WebMocks.CashgameChartPageModelFactoryMock.Object,
                WebMocks.CashgameDetailsPageModelFactoryMock.Object,
                WebMocks.CashgameEditPageModelFactoryMock.Object,
                WebMocks.CashgameFactsPageModelFactoryMock.Object,
                WebMocks.CashgameLeaderboardPageModelFactoryMock.Object,
                WebMocks.CashgameListingPageModelFactoryMock.Object,
                WebMocks.RunningCashgamePageModelFactoryMock.Object,
                WebMocks.CashgameModelMapperMock.Object,
                WebMocks.CheckpointModelMapperMock.Object);
        }

	}

}