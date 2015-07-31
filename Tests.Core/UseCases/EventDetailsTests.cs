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
            var result = Sut.Execute(input);

            Assert.AreEqual(TestData.EventNameA, result.Name);
        }

        private EventDetailsInteractor Sut
        {
            get
            {
                return new EventDetailsInteractor(
                    Repos.Event);
            }
        }
    }
}
