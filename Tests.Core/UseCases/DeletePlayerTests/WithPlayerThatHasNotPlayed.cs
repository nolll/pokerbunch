using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.DeletePlayerTests
{
    public class WithPlayerThatHasNotPlayed : Arrange
    {
        protected override string PlayerId => IdForPlayerThatHasNotPlayed;
            
        [Test]
        public void DeletePlayer_PlayerHasntPlayed_PlayerDeletedAndReturnUrlIsPlayerIndex()
        {
            Assert.IsTrue(Result.Deleted);
            Assert.AreEqual(BunchData.Id1, Result.Slug);
            Assert.AreEqual(IdForPlayerThatHasNotPlayed, Result.PlayerId);
            Assert.AreEqual(IdForPlayerThatHasNotPlayed, DeletedId);
        }
    }
}
