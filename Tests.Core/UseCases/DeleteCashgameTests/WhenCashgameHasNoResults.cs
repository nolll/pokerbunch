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
            Sut.Execute(Request);

            Assert.AreEqual(Id, DeletedId);
        }

        [Test]
        public void BunchIdIsSet()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(BunchData.Id1, result.Slug);
        }
    }
}