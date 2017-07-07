using Core.Entities;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.EventListTests
{
    public class WhenExecute : Arrange
    {
        [Test]
        public void EventList_ReturnsAllEvents()
        {
            Assert.AreEqual(2, Result.Events.Count);
        }

        [Test]
        public void EventList_EachItem_NameIsSet()
        {
            Assert.AreEqual(EventData.Name2, Result.Events[0].Name);
            Assert.AreEqual(EventData.Name1, Result.Events[1].Name);
        }

        [Test]
        public void EventList_EachItem_StartDateIsSet()
        {
            Assert.AreEqual(new Date(2001, 2, 1).IsoString, Result.Events[0].StartDate.IsoString);
            Assert.AreEqual(new Date(2001, 1, 1).IsoString, Result.Events[1].StartDate.IsoString);
        }

        [Test]
        public void IdIsSet()
        {
            Assert.AreEqual(EventData.Id2, Result.Events[0].EventId);
            Assert.AreEqual(EventData.Id1, Result.Events[1].EventId);
        }
    }
}
