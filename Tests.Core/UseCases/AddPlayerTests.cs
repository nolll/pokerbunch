using System.Linq;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Urls;
using Core.UseCases.AddPlayer;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class AddPlayerTests : TestBase
    {
        private const string InvalidName = "";
        private const string Name = "b";

        [Test]
        public void AddPlayer_ReturnUrlIsSet()
        {
            var request = new AddPlayerRequest(Constants.SlugA, Name);
            var result = Execute(request);

            Assert.IsInstanceOf<AddPlayerConfirmationUrl>(result.ReturnUrl);
        }

        [Test]
        public void AddPlayer_EmptyName_ThrowsException()
        {
            var request = new AddPlayerRequest(Constants.SlugA, InvalidName);

            var ex = Assert.Throws<ValidationException>(() => Execute(request));
            Assert.AreEqual(1, ex.Messages.Count());
        }

        [Test]
        public void AddPlayer_ValidName_AddsPlayer()
        {
            var request = new AddPlayerRequest(Constants.SlugA, Name);
            Execute(request);

            GetMock<IPlayerRepository>().Verify(o => o.Add(It.IsAny<Player>()));
        }

        [Test]
        public void AddPlayer_ValidNameButNameExists_ThrowsException()
        {
            var player = A.Player.Build();

            GetMock<IPlayerRepository>().Setup(o => o.GetByName(It.IsAny<int>(), Name)).Returns(player);

            var request = new AddPlayerRequest(Constants.SlugA, Name);
            Assert.Throws<PlayerExistsException>(() => Execute(request));
        }

        private AddPlayerResult Execute(AddPlayerRequest request)
        {
            return AddPlayerInteractor.Execute(
                Repo.Bunch,
                GetMock<IPlayerRepository>().Object,
                request);
        }
    }
}
