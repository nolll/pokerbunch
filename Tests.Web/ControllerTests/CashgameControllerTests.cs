using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Application.Services;
using Core.Entities;
using Core.Repositories;
using Core.Services.Interfaces;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.Commands.CashgameCommands;
using Web.Controllers;
using Web.ModelFactories.CashgameModelFactories.Action;
using Web.ModelFactories.CashgameModelFactories.Facts;
using Web.ModelFactories.CashgameModelFactories.Toplist;
using Web.ModelServices;

namespace Tests.Web.ControllerTests
{
	public class CashgameControllerTests : MockContainer
	{
        private const string Slug = "homegame1";
        private const string DateStr = "2010-01-01";
        private const string PlayerName = "Player 1";
	    private const int PlayerId = 1;

		[Test]
		public void Matrix_CorrectView(){
		    var sut = GetSut();
            var viewResult = (ViewResult)sut.Matrix(Slug);

			Assert.AreEqual("Matrix/MatrixPage", viewResult.ViewName);
		}

        [Test]
        public void Toplist_Authorized_ShowsCorrectView()
        {
		    var sut = GetSut();
            var viewResult = (ViewResult)sut.Toplist(Slug);

            Assert.AreEqual("Toplist/ToplistPage", viewResult.ViewName);
		}

		[Test]
		public void Details_ReturnsCorrectView(){
            var sut = GetSut();
            var viewResult = (ViewResult)sut.Details(Slug, DateStr);

            Assert.AreEqual("Details/DetailsPage", viewResult.ViewName);
		}

        [Test]
        public void Action_RequiresPlayer()
        {
            var sut = GetSut();
            Func<string, string, int, ActionResult> methodToTest = sut.Action;
            var result = SecurityTestHelper.RequiresPlayer(methodToTest);

            Assert.IsTrue(result);
        }

		[Test]
		public void Action_ReturnsCorrectModel()
		{
		    var homegame = new HomegameInTest();
		    var user = new UserInTest();
            var player = new PlayerInTest(1);
		    var cashgameResult = new CashgameResultInTest();
		    var cashgame = new CashgameInTest(results: new List<CashgameResult> {cashgameResult});
            GetMock<IHomegameRepository>().Setup(o => o.GetBySlug(Slug)).Returns(homegame);
            GetMock<IAuth>().Setup(o => o.CurrentUser).Returns(user);
            GetMock<ICashgameRepository>().Setup(o => o.GetByDateString(homegame, DateStr)).Returns(cashgame);
            GetMock<IPlayerRepository>().Setup(o => o.GetById(PlayerId)).Returns(player);

			var sut = GetSut();
            var viewResult = (ViewResult)sut.Action(Slug, DateStr, PlayerId);

            Assert.AreEqual("Action/Action", viewResult.ViewName);
		}

        [Test]
        public void Buyin_RequiresOwnPlayer()
        {
            var sut = GetSut();
            Func<string, int, ActionResult> methodToTest = sut.Buyin;
            var result = SecurityTestHelper.RequiresOwnPlayer(methodToTest);

            Assert.IsTrue(result);
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
                GetMock<ICashgameService>().Object,
                GetMock<ICashgameCommandProvider>().Object,
                GetMock<ICashgameModelService>().Object,
                GetMock<IToplistPageBuilder>().Object,
                GetMock<ICashgameFactsPageBuilder>().Object,
                GetMock<IActionPageBuilder>().Object);
        }
	}
}