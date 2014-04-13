using System.Collections.Generic;
using Application.Services;
using Core.UseCases.PlayerList;
using NUnit.Framework;
using Tests.Common;
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

        [Test]
        public void CreateList_ReturnsListOfCorrectLength()
        {
            const string slug = "a";
            var playerListItem = new PlayerListItem();
            var playerListItems = new List<PlayerListItem> {playerListItem};

            var result = _sut.CreateList(slug, playerListItems);

            Assert.AreEqual(1, result.Count);
        }
    }
}
