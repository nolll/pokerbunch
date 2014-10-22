using Core.Entities;
using Core.Repositories;
using Core.UseCases.EventDetails;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class EventDetailsTests : TestBase
    {
        private const int EventId = 1;
        private const string EventName = "a";

        [Test]
        public void EventDetails_NameIsSet()
        {
            GetMock<IEventRepository>().Setup(o => o.GetById(EventId)).Returns(new Event(EventId, EventName));

            var input = new EventDetailsInput(1);
            var result = Execute(input);

            Assert.AreEqual(EventName, result.Name);
        }

        private EventDetailsOutput Execute(EventDetailsInput input)
        {
            return EventDetailsInteractor.Execute(
                GetMock<IEventRepository>().Object,
                input);
        }
    }
}
