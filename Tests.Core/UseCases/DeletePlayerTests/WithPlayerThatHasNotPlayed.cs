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
            var result = Sut.Execute(Request);

            Assert.IsTrue(result.Deleted);
            Assert.AreEqual(BunchData.Id1, result.Slug);
            Assert.AreEqual(IdForPlayerThatHasNotPlayed, result.PlayerId);
            Assert.AreEqual(IdForPlayerThatHasNotPlayed, DeletedId);
        }
    }
}
