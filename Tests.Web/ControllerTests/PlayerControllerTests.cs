using System.Web.Mvc;
using Application.Exceptions;
using Application.Services.Interfaces;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeCommands;
using Web.Commands.PlayerCommands;
using Web.Controllers;
using Web.ModelServices;

namespace Tests.Web.ControllerTests{

	public class PlayerControllerTests : MockContainer
    {
        [Test]
		public void Details_NotAuthorized_ThrowsException()
		{
		    const string slug = "a";
		    const string playerName = "b";
            GetMock<IAuthorization>().Setup(o => o.RequirePlayer(slug)).Throws<AccessDeniedException>();

		    var sut = GetSut();

            Assert.Throws<AccessDeniedException>(() => sut.Details(slug, playerName));
		}

        [Test]
		public void Delete_NotAuthorized_ThrowsException()
        {
			const string slug = "a";
		    const string playerName = "b";
            GetMock<IAuthorization>().Setup(o => o.RequireManager(slug)).Throws<AccessDeniedException>();

            var sut = GetSut();

            Assert.Throws<AccessDeniedException>(() => sut.Delete(slug, playerName));
		}
        
        [Test]
		public void Delete_WithSuccessfulCommand_RedirectsToPlayerList()
        {
            const string slug = "a";
		    const string playerName = "b";
            const string listUrl = "c";

            GetMock<IPlayerCommandProvider>().Setup(o => o.GetDeleteCommand(slug, playerName)).Returns(new FakeSuccessfulCommand());
            GetMock<IUrlProvider>().Setup(o => o.GetPlayerIndexUrl(slug)).Returns(listUrl);

            var sut = GetSut();
            var result = sut.Delete(slug, playerName) as RedirectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(listUrl, result.Url);
        }

        [Test]
        public void Delete_WithFailedCommand_RedirectsToPlayerList()
        {
            const string slug = "a";
            const string playerName = "b";
            const string playerUrl = "c";

            GetMock<IPlayerCommandProvider>().Setup(o => o.GetDeleteCommand(slug, playerName)).Returns(new FakeFailedCommand());
            GetMock<IUrlProvider>().Setup(o => o.GetPlayerDetailsUrl(slug, playerName)).Returns(playerUrl);

            var sut = GetSut();
            var result = sut.Delete(slug, playerName) as RedirectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(playerUrl, result.Url);
        }

        private PlayerController GetSut(){
			return new PlayerController(
                GetMock<IAuthentication>().Object,
                GetMock<IAuthorization>().Object,
                GetMock<IPlayerModelService>().Object,
                GetMock<IUrlProvider>().Object,
                GetMock<IPlayerCommandProvider>().Object);
		}

	}

}