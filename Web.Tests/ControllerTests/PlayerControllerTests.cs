using System.Web.Mvc;
using Core.Classes;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using Infrastructure.Repositories;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Tests.Common.FakeCommands;
using Web.Commands.PlayerCommands;
using Web.Controllers;
using Web.ModelServices;

namespace Web.Tests.ControllerTests{

	public class PlayerControllerTests : MockContainer
    {
        [Test]
		public void Details_NotAuthorized_ThrowsException()
		{
		    const string homegameName = "a";
		    const string playerName = "b";
            GetMock<IHomegameRepository>().Setup(o => o.GetByName(homegameName)).Returns(new FakeHomegame());
            GetMock<IUserContext>().Setup(o => o.RequirePlayer(It.IsAny<Homegame>())).Throws<AccessDeniedException>();

		    var sut = GetSut();

            Assert.Throws<AccessDeniedException>(() => sut.Details(homegameName, playerName));
		}

        [Test]
		public void Delete_NotAuthorized_ThrowsException()
        {
			const string homegameName = "a";
		    const string playerName = "b";
            GetMock<IHomegameRepository>().Setup(o => o.GetByName(homegameName)).Returns(new FakeHomegame());
            GetMock<IUserContext>().Setup(o => o.RequireManager(It.IsAny<Homegame>())).Throws<AccessDeniedException>();

            var sut = GetSut();

            Assert.Throws<AccessDeniedException>(() => sut.Delete(homegameName, playerName));
		}
        
        [Test]
		public void Delete_WithSuccessfulCommand_RedirectsToPlayerListing()
        {
            const string homegameName = "a";
		    const string playerName = "b";
            const string listingUrl = "c";

            GetMock<IPlayerCommandProvider>().Setup(o => o.GetDeleteCommand(It.IsAny<Homegame>(), It.IsAny<Player>())).Returns(new FakeSuccessfulCommand());
            GetMock<IUrlProvider>().Setup(o => o.GetPlayerIndexUrl(It.IsAny<Homegame>())).Returns(listingUrl);

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

            GetMock<IPlayerCommandProvider>().Setup(o => o.GetDeleteCommand(It.IsAny<Homegame>(), It.IsAny<Player>())).Returns(new FakeFailedCommand());
            GetMock<IUrlProvider>().Setup(o => o.GetPlayerDetailsUrl(It.IsAny<Homegame>(), It.IsAny<Player>())).Returns(playerUrl);

            var sut = GetSut();
            var result = sut.Delete(homegameName, playerName) as RedirectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(playerUrl, result.Url);
        }

        private PlayerController GetSut(){
			return new PlayerController(
                GetMock<IUserContext>().Object,
                GetMock<IHomegameRepository>().Object,
                GetMock<IPlayerRepository>().Object, 
                GetMock<IPlayerModelService>().Object,
                GetMock<IUrlProvider>().Object,
                GetMock<IPlayerCommandProvider>().Object);
		}

	}

}