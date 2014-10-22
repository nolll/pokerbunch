using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.UseCases.EventList;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class EventListTests : TestBase
    {
        private const string Slug = "a";
        private const int BunchId = 1;

        [Test]
        public void EventList_ReturnsAllEvents()
        {
            SetupEventList();

            var result = Execute(CreateInput());

            Assert.AreEqual(2, result.Events.Count);
        }

        [Test]
        public void EventList_EachItem_NameIsSet()
        {
            SetupEventList();

            var result = Execute(CreateInput());

            Assert.AreEqual("Event 1", result.Events[0].Name);
            Assert.AreEqual("Event 2", result.Events[1].Name);
        }

        [Test]
        public void EventList_EachItem_UrlIsSet()
        {
            SetupEventList();

            var result = Execute(CreateInput());

            Assert.AreEqual("/a/event/details/1", result.Events[0].EventDetailsUrl.Relative);
            Assert.AreEqual("/a/event/details/2", result.Events[1].EventDetailsUrl.Relative);
        }

        private void SetupEventList()
        {
            var bunch = A.Bunch.WithId(BunchId).WithSlug(Slug).Build();
            GetMock<IBunchRepository>().Setup(o => o.GetBySlug(Slug)).Returns(bunch);
            var eventList = new List<Event> {new Event(1, "Event 1"), new Event(2, "Event 2")};
            GetMock<IEventRepository>().Setup(o => o.Find(BunchId)).Returns(eventList);
        }

        private EventListOutput Execute(EventListInput input)
        {
            return EventListInteractor.Execute(
                GetMock<IBunchRepository>().Object,
                GetMock<IEventRepository>().Object,
                input);
        }

        private EventListInput CreateInput()
        {
            return new EventListInput(Slug);
        }
    }
}
