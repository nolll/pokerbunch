using System.Collections.Generic;
using Application.Services;
using Application.UseCases.BunchList;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.HomegameModelFactories;

namespace Tests.Web.ModelFactoryTests.HomegameModelFactories
{
    class BunchListItemModelFactoryTests : MockContainer
    {
        private BunchListItemModelFactory _sut;

        [SetUp]
        public virtual void SetUp()
        {
            _sut = new BunchListItemModelFactory();
        }

        [Test]
        public void Create_WithBunchItem_AllPropertiesAreSet()
        {
            const string displayName = "a";
            const string slug = "b";
            var bunchItem = new BunchListItem
                {
                    DisplayName = displayName,
                    Slug = slug
                };

            var result = _sut.Create(bunchItem);

            Assert.AreEqual(displayName, result.Name);
        }

        [Test]
        public void CreateList_WithBunchItems_ReturnsListOfCorrectLength()
        {
            var bunchItems = new List<BunchListItem> {new BunchListItem()};

            var result = _sut.CreateList(bunchItems);

            Assert.AreEqual(1, result.Count);
        }
    }
}
