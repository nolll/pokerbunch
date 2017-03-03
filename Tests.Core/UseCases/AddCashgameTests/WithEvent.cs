using NUnit.Framework;

namespace Tests.Core.UseCases.AddCashgameTests
{
    public class WithEvent : Arrange
    {
        protected override string EventId => ExistingEventId;

        [Test]
        public void GameIsAddedToEvent()
        {
            Assert.AreEqual(EventId, EventIdThatCashgameWasAddedTo);
            Assert.AreEqual(GeneratedCashgameId, CashgameIdAddedToEvent);
        }
    }
}