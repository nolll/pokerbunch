using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.DeletePlayerTests
{
    public class WithPlayerThatHasPlayed : Arrange
    {
        protected override string PlayerId => IdForPlayerThatHasPlayed;

        [Test]
        public void DeletePlayer_PlayerHasPlayed_ReturnUrlIsPlayerDetails()
        {
            Assert.IsFalse(Result.Deleted);
            Assert.AreEqual(BunchData.Id1, Result.Slug);
            Assert.AreEqual(IdForPlayerThatHasPlayed, Result.PlayerId);
        }
    }
}