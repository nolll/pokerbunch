using System;
using System.Web.Mvc;
using Core.Classes;
using Core.Exceptions;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Web.Controllers;

namespace Web.Tests.ControllerTests{

	public class CashgameControllerTests : MockContainer {

        [Test]
		public void Matrix_NotAuthorized_ThrowsException(){
			HomegameRepositoryMock.Setup(o => o.GetByName("homegame1")).Returns(new Homegame());
            UserContextMock.Setup(o => o.RequirePlayer(It.IsAny<Homegame>())).Throws<AccessDeniedException>();

            var sut = GetSut();

            Assert.Throws<AccessDeniedException>(() => sut.Matrix("homegame1"));
		}

		[Test]
		public void Matrix_Authorized_ShowsCorrectView(){
			HomegameRepositoryMock.Setup(o => o.GetByName("homegame1")).Returns(new Homegame());
		    UserContextMock.Setup(o => o.GetUser()).Returns(new User());

		    var sut = GetSut();
			var viewResult = (ViewResult)sut.Matrix("homegame1");

			Assert.AreEqual("Matrix/MatrixPage", viewResult.ViewName);
		}

        [Test]
		public void Leaderboard_NotAuthorized_ThrowsException(){
			HomegameRepositoryMock.Setup(o => o.GetByName("homegame1")).Returns(new Homegame());
            UserContextMock.Setup(o => o.RequirePlayer(It.IsAny<Homegame>())).Throws<AccessDeniedException>();

            var sut = GetSut();

			Assert.Throws<AccessDeniedException>(() => sut.Leaderboard("homegame1"));
		}

        [Test]
		public void Leaderboard_Authorized_ShowsCorrectView(){
			HomegameRepositoryMock.Setup(o => o.GetByName("homegame1")).Returns(new Homegame());
            CashgameRepositoryMock.Setup(o => o.GetSuite(It.IsAny<Homegame>(), It.IsAny<int?>())).Returns(new CashgameSuite());
		    UserContextMock.Setup(o => o.GetUser()).Returns(new User());

		    var sut = GetSut();
			var viewResult = (ViewResult)sut.Leaderboard("homegame1");

			Assert.AreEqual("Leaderboard/LeaderboardPage", viewResult.ViewName);
		}

        [Test]
		public void Details_NotAuthorized_ThrowsException(){
            HomegameRepositoryMock.Setup(o => o.GetByName("homegame1")).Returns(new Homegame());
            UserContextMock.Setup(o => o.RequirePlayer(It.IsAny<Homegame>())).Throws<AccessDeniedException>();

            var sut = GetSut();

            Assert.Throws<AccessDeniedException>(() => sut.Details("homegame1", "2010-01-01"));
		}

		[Test]
		public void Details_ReturnsCorrectView(){
			HomegameRepositoryMock.Setup(o => o.GetByName("homegame1")).Returns(new Homegame());
		    UserContextMock.Setup(o => o.GetUser()).Returns(new User());
		    CashgameRepositoryMock.Setup(o => o.GetByDate(It.IsAny<Homegame>(), It.IsAny<DateTime>())).Returns(new Cashgame());
		    PlayerRepositoryMock.Setup(o => o.GetByUserName(It.IsAny<Homegame>(), It.IsAny<string>())).Returns(new Player());
            
            var sut = GetSut();
            var viewResult = (ViewResult)sut.Details("homegame1", "2010-01-01");

            Assert.AreEqual("Details/DetailsPage", viewResult.ViewName);
		}

        private CashgameController GetSut()
        {
            return new CashgameController(
                HomegameRepositoryMock.Object,
                UserContextMock.Object, 
                CashgameRepositoryMock.Object, 
                PlayerRepositoryMock.Object, 
                MatrixPageModelFactoryMock.Object,
                CashgameFactoryMock.Object,
                TimeProviderMock.Object,
                BuyinPageModelFactoryMock.Object,
                ReportPageModelFactoryMock.Object,
                CashoutPageModelFactoryMock.Object,
                EndPageModelFactoryMock.Object,
                ActionPageModelFactoryMock.Object,
                AddCashgamePageModelFactoryMock.Object,
                CashgameChartPageModelFactoryMock.Object,
                CashgameDetailsPageModelFactoryMock.Object,
                CashgameEditPageModelFactoryMock.Object,
                CashgameFactsPageModelFactoryMock.Object,
                CashgameLeaderboardPageModelFactoryMock.Object,
                CashgameListingPageModelFactoryMock.Object,
                RunningCashgamePageModelFactoryMock.Object,
                CashgameModelMapperMock.Object);
        }

	}

}