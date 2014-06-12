using Application.UseCases.BunchList;
using NUnit.Framework;
using Tests.Common;
using Web.Models.HomegameModels.List;

namespace Tests.Web.ModelFactoryTests.HomegameModelFactories
{
    class BunchListItemModelTests : MockContainer
    {
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

            var result = new BunchListItemModel(bunchItem);

            Assert.AreEqual(displayName, result.Name);
        }
    }
}
