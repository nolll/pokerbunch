using Core.UseCases.EventDetails;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class EventDetailsTests : TestBase
    {
        [Test]
        public void EventDetails_NameIsSet()
        {
            var input = new EventDetailsInput(1);
            var result = Execute(input);

            Assert.AreEqual(Constants.EventNameA, result.Name);
        }

        private EventDetailsOutput Execute(EventDetailsInput input)
        {
            return EventDetailsInteractor.Execute(
                Repos.Event,
                input);
        }
    }
}
