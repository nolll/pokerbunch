using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.DeleteCashgameTests
{
    public class WhenCashgameHasNoResults : Arrange
    {
        protected override string Id => IdWithoutResults;

        [Test]
        public void DeletesGame()
        {
            Execute();
            Assert.AreEqual(Id, DeletedId);
        }

        [Test]
        public void BunchIdIsSet() => Assert.AreEqual(BunchData.Id1, Execute().Slug);
    }
}