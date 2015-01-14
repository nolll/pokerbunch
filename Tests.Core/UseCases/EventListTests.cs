using Core.UseCases.EventList;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class EventListTests : TestBase
    {
        [Test]
        public void EventList_ReturnsAllEvents()
        {
            var result = Execute(CreateInput());

            Assert.AreEqual(2, result.Events.Count);
        }

        [Test]
        public void EventList_EachItem_NameIsSet()
        {
            var result = Execute(CreateInput());

            Assert.AreEqual(Constants.EventNameA, result.Events[0].Name);
            Assert.AreEqual(Constants.EventNameB, result.Events[1].Name);
        }

        [Test]
        public void EventList_EachItem_UrlIsSet()
        {
            var result = Execute(CreateInput());

            Assert.AreEqual("/bunch-a/event/details/1", result.Events[0].EventDetailsUrl.Relative);
            Assert.AreEqual("/bunch-a/event/details/2", result.Events[1].EventDetailsUrl.Relative);
        }
        
        private EventListOutput Execute(EventListInput input)
        {
            return EventListInteractor.Execute(
                Repos.Bunch,
                Repos.Event,
                input);
        }

        private EventListInput CreateInput()
        {
            return new EventListInput(Constants.SlugA);
        }
    }
}
