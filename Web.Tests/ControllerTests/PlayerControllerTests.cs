using System.Web.Mvc;
using Core.Classes;
using Core.Exceptions;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Tests.Common.FakeCommands;
using Web.Controllers;

namespace Web.Tests.ControllerTests{

	public class PlayerControllerTests : WebMockContainer
    {
        [Test]
		public void Details_NotAuthorized_ThrowsException()
		{
		    const string homegameName = "a";
		    const string playerName = "b";
		    Mocks.HomegameRepositoryMock.Setup(o => o.GetByName(homegameName)).Returns(new FakeHomegame());
            Mocks.UserContextMock.Setup(o => o.RequirePlayer(It.IsAny<Homegame>())).Throws<AccessDeniedException>();

		    var sut = GetSut();

            Assert.Throws<AccessDeniedException>(() => sut.Details(homegameName, playerName));
		}

        [Test]
		public void Delete_NotAuthorized_ThrowsException()
        {
			const string homegameName = "a";
		    const string playerName = "b";
            Mocks.HomegameRepositoryMock.Setup(o => o.GetByName(homegameName)).Returns(new FakeHomegame());
            Mocks.UserContextMock.Setup(o => o.RequireManager(It.IsAny<Homegame>())).Throws<AccessDeniedException>();

            var sut = GetSut();

            Assert.Throws<AccessDeniedException>(() => sut.Delete(homegameName, playerName));
		}
        
        [Test]
		public void Delete_WithSuccessfulCommand_RedirectsToPlayerListing()
        {
            const string homegameName = "a";
		    const string playerName = "b";
            const string listingUrl = "c";

            Mocks.PlayerCommandProviderMock.Setup(o => o.GetDeleteCommand(It.IsAny<Homegame>(), It.IsAny<Player>())).Returns(new FakeSuccessfulCommand());
            Mocks.UrlProviderMock.Setup(o => o.GetPlayerIndexUrl(It.IsAny<Homegame>())).Returns(listingUrl);

            var sut = GetSut();
            var result = sut.Delete(homegameName, playerName) as RedirectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(listingUrl, result.Url);
        }

        [Test]
        public void Delete_WithFailedCommand_RedirectsToPlayerListing()
        {
            const string homegameName = "a";
            const string playerName = "b";
            const string playerUrl = "c";

            Mocks.PlayerCommandProviderMock.Setup(o => o.GetDeleteCommand(It.IsAny<Homegame>(), It.IsAny<Player>())).Returns(new FakeFailedCommand());
            Mocks.UrlProviderMock.Setup(o => o.GetPlayerDetailsUrl(It.IsAny<Homegame>(), It.IsAny<Player>())).Returns(playerUrl);

            var sut = GetSut();
            var result = sut.Delete(homegameName, playerName) as RedirectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(playerUrl, result.Url);
        }

        private PlayerController GetSut(){
			return new PlayerController(
                Mocks.UserContextMock.Object,
                Mocks.HomegameRepositoryMock.Object,
                Mocks.PlayerRepositoryMock.Object, 
                Mocks.PlayerModelServiceMock.Object,
                Mocks.UrlProviderMock.Object,
                Mocks.PlayerCommandProviderMock.Object);
		}

	}

}