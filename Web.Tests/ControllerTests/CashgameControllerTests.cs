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
		public void ActionMatrix_NotAuthorized_ThrowsException(){
			HomegameRepositoryMock.Setup(o => o.GetByName("homegame1")).Returns(new Homegame());
            UserContextMock.Setup(o => o.RequirePlayer(It.IsAny<Homegame>())).Throws<AccessDeniedException>();

            var sut = GetSut();

            Assert.Throws<AccessDeniedException>(() => sut.Matrix("homegame1"));
		}

		[Test]
		public void ActionMatrix_Authorized_ShowsCorrectView(){
			HomegameRepositoryMock.Setup(o => o.GetByName("homegame1")).Returns(new Homegame());
		    UserContextMock.Setup(o => o.GetUser()).Returns(new User());

		    var sut = GetSut();
			var viewResult = (ViewResult)sut.Matrix("homegame1");

			Assert.AreEqual("Matrix/MatrixPage", viewResult.ViewName);
		}

        private CashgameController GetSut()
        {
            return new CashgameController(HomegameRepositoryMock.Object, UserContextMock.Object, CashgameRepositoryMock.Object, MatrixPageModelFactoryMock.Object);
        }

	}

}