using Application.Services;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Tests.Core.UseCases;
using Web.ModelFactories.PlayerModelFactories;

namespace Tests.Web.ModelFactoryTests.PlayerModelFactories
{
    class PlayerItemModelFactoryTests : MockContainer
    {
        private PlayerItemModelFactory _sut;

        [SetUp]
        public virtual void SetUp()
        {
            _sut = new PlayerItemModelFactory(
                GetMock<IUrlProvider>().Object);
        }

        [Test]
        public void Create_WithHomegameAndPlayer_NameAndUrlIsSet()
        {
            const string slug = "a";
            const string displayName = "b";
            const string url = "c";
            var homegame = new FakeHomegame(slug: slug);
            var player = new FakePlayer(displayName: displayName);

            GetMock<IUrlProvider>().Setup(o => o.GetPlayerDetailsUrl(slug, displayName)).Returns(url);

            var result = _sut.Create(homegame, player);

            Assert.AreEqual(displayName, result.Name);
            Assert.AreEqual(url, result.Url);
        }

        [Test]
        public void Create_WithPlayerListItem_NameAndUrlIsSet()
        {
            const string slug = "a";
            const string displayName = "b";
            const string url = "c";
            var playerListItem = new PlayerListItem{Name = displayName};

            GetMock<IUrlProvider>().Setup(o => o.GetPlayerDetailsUrl(slug, displayName)).Returns(url);

            var result = _sut.Create(slug, playerListItem);

            Assert.AreEqual(displayName, result.Name);
            Assert.AreEqual(url, result.Url);
        }
    }
}
