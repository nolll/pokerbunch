using System.Web.Mvc;
using Core.Classes;
using Core.Exceptions;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Web.Controllers;
using Web.Models.CashgameModels.Leaderboard;

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
		public void ActionLeaderboard_SetsTableModel(){
			HomegameRepositoryMock.Setup(o => o.GetByName("homegame1")).Returns(new Homegame());
			CashgameRepositoryMock.Setup(o => o.GetSuite(It.IsAny<Homegame>(), It.IsAny<int?>())).Returns(new CashgameSuite());

            var sut = GetSut();
			var result = sut.Leaderboard("homegame1") as ViewResult;

            Assert.IsNotNull(result);
		    var model = result.Model as LeaderboardPageModel;
            Assert.IsNotNull(model);
			Assert.IsNotNull(model.TableModel);
		}

        private CashgameController GetSut()
        {
            return new CashgameController(HomegameRepositoryMock.Object, UserContextMock.Object, CashgameRepositoryMock.Object, MatrixPageModelFactoryMock.Object);
        }

	}

}