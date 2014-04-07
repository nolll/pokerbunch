using Application.Services;
using Core.UseCases;
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
            _sut = new BunchListItemModelFactory(
                GetMock<IUrlProvider>().Object);
        }

        [Test]
        public void Create_WithBunchItem_AllPropertiesAreSet()
        {
            const string displayName = "a";
            const string slug = "b";
            const string url = "c";
            var bunchItem = new BunchItem
                {
                    DisplayName = displayName,
                    Slug = slug
                };

            GetMock<IUrlProvider>().Setup(o => o.GetHomegameDetailsUrl(slug)).Returns(url);

            var result = _sut.Create(bunchItem);

            Assert.AreEqual(displayName, result.Name);
        }
    }
}
