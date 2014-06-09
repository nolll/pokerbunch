using System.Collections.Generic;
using Application.Services;
using Application.UseCases.PlayerList;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.PlayerModelFactories;
using Web.Models.UrlModels;

namespace Tests.Web.ModelFactoryTests.PlayerModelFactories
{
    class PlayerItemModelFactoryTests : MockContainer
    {
        private PlayerItemModelFactory _sut;

        [SetUp]
        public virtual void SetUp()
        {
            _sut = new PlayerItemModelFactory();
        }

        [Test]
        public void Create_WithPlayerListItem_NameAndUrlIsSet()
        {
            const string slug = "a";
            const string displayName = "b";
            const int id = 1;
            var playerListItem = new PlayerListItem{Id = id, Name = displayName};

            var result = _sut.Create(slug, playerListItem);

            Assert.AreEqual(displayName, result.Name);
            Assert.IsInstanceOf<PlayerDetailsUrlModel>(result.Url);
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
