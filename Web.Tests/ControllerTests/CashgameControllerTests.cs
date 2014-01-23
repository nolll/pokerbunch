using System.Collections.Generic;
using System.Web.Mvc;
using Core.Classes;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.Commands.CashgameCommands;
using Web.Controllers;
using Web.ModelServices;

namespace Web.Tests.ControllerTests{

	public class CashgameControllerTests : MockContainer
	{
        private const string Slug = "homegame1";
        private const string DateStr = "2010-01-01";
        private const string PlayerName = "Player 1";
        private const string UserName = "user1";

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
		public void Action_NotAuthorized_ThrowsException(){
            GetMock<IHomegameRepository>().Setup(o => o.GetByName(Slug)).Returns(new FakeHomegame());
            GetMock<IAuthorization>().Setup(o => o.RequirePlayer(Slug)).Throws<AccessDeniedException>();

            var sut = GetSut();

            Assert.Throws<AccessDeniedException>(() => sut.Action(Slug, DateStr, PlayerName));
		}

        [Test]
        public void Action_RequiresUserAndPlayer()
        {
            var sut = GetSut();
            sut.Action(Slug, DateStr, PlayerName);

            GetMock<IAuthentication>().Verify(o => o.RequireUser());
            GetMock<IAuthorization>().Verify(o => o.RequirePlayer(Slug));
        }

		[Test]
		public void Action_ReturnsCorrectModel()
		{
		    var homegame = new FakeHomegame();
		    var user = new FakeUser();
            var player = new FakePlayer(1);
		    var cashgameResult = new FakeCashgameResult();
		    var cashgame = new FakeCashgame(results: new List<CashgameResult> {cashgameResult});
            GetMock<IHomegameRepository>().Setup(o => o.GetByName(Slug)).Returns(homegame);
            GetMock<IAuthentication>().Setup(o => o.GetUser()).Returns(user);
            GetMock<ICashgameRepository>().Setup(o => o.GetByDateString(homegame, DateStr)).Returns(cashgame);
            GetMock<IPlayerRepository>().Setup(o => o.GetByName(homegame, PlayerName)).Returns(player);

			var sut = GetSut();
            var viewResult = (ViewResult)sut.Action(Slug, DateStr, PlayerName);

            Assert.AreEqual("Action/Action", viewResult.ViewName);
		}

        [Test]
		public void ActionBuyin_NotAuthorized_ThrowsException()
        {
            GetMock<IAuthorization>().Setup(o => o.RequirePlayer(Slug)).Throws<AccessDeniedException>();

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
		    var user = new FakeUser(firstUserId);
			var player = new FakePlayer(userId: secondUserId);

            GetMock<IHomegameRepository>().Setup(o => o.GetByName(Slug)).Returns(homegame);
            GetMock<ICashgameRepository>().Setup(o => o.GetRunning(homegame)).Returns(cashgame);
            GetMock<IAuthentication>().Setup(o => o.GetUser()).Returns(user);
            GetMock<IPlayerRepository>().Setup(o => o.GetByName(homegame, PlayerName)).Returns(player);

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
                GetMock<IAuthentication>().Object,
                GetMock<IAuthorization>().Object,
                GetMock<IUrlProvider>().Object,
                GetMock<ICashgameService>().Object,
                GetMock<ICashgameCommandProvider>().Object,
                GetMock<ICashgameModelService>().Object);
        }

	}

}