using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.EditCashgameTests
{
    public class WithValidInput : Arrange
    {
        [Test]
        public void RepositoryIsCalledWithSavedValues()
        {
            Sut.Execute(ValidRequest);

            Assert.AreEqual(LocationData.Id1, SavedLocationId);
            Assert.AreEqual(EventData.Id1, SavedEventId);
        }
    }
}