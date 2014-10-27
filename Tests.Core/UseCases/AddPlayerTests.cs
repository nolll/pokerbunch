using System.Linq;
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
        private const string Slug = "a";
        private const string InvalidName = "";
        private const string Name = "b";

        [Test]
        public void AddPlayer_ReturnUrlIsSet()
        {
            SetupBunch();
            
            var request = new AddPlayerRequest(Slug, Name);
            var result = Execute(request);

            Assert.IsInstanceOf<AddPlayerConfirmationUrl>(result.ReturnUrl);
        }

        [Test]
        public void AddPlayer_EmptyName_ThrowsException()
        {
            var request = new AddPlayerRequest(Slug, InvalidName);

            var ex = Assert.Throws<ValidationException>(() => Execute(request));
            Assert.AreEqual(1, ex.Messages.Count());
        }

        [Test]
        public void AddPlayer_ValidName_AddsPlayer()
        {
            SetupBunch();
            
            var request = new AddPlayerRequest(Slug, Name);
            Execute(request);

            GetMock<IPlayerRepository>().Verify(o => o.Add(It.IsAny<int>(), Name));
        }

        [Test]
        public void AddPlayer_ValidNameButNameExists_ThrowsException()
        {
            SetupBunch();
            var player = A.Player.Build();

            GetMock<IPlayerRepository>().Setup(o => o.GetByName(It.IsAny<int>(), Name)).Returns(player);
            
            var request = new AddPlayerRequest(Slug, Name);
            Assert.Throws<PlayerExistsException>(() => Execute(request));
        }

        private void SetupBunch()
        {
            var bunch = A.Bunch.Build();
            GetMock<IBunchRepository>().Setup(o => o.GetBySlug(It.IsAny<string>())).Returns(bunch);
        }

        private AddPlayerResult Execute(AddPlayerRequest request)
        {
            return AddPlayerInteractor.Execute(
                GetMock<IBunchRepository>().Object,
                GetMock<IPlayerRepository>().Object,
                request);
        }
    }
}
