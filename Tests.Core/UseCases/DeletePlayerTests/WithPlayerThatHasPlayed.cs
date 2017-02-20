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
            var result = Sut.Execute(Request);

            Assert.IsFalse(result.Deleted);
            Assert.AreEqual(BunchData.Id1, result.Slug);
            Assert.AreEqual(IdForPlayerThatHasPlayed, result.PlayerId);
        }
    }
}