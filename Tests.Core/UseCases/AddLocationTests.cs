using Core.Exceptions;
using Core.UseCases;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    public class AddLocationTests : TestBase
    {
        [Test]
        public void AddLocation_AllOk_LocationIsAdded()
        {
            const string addedEventName = "added location";

            var request = new AddLocation.Request(TestData.BunchA.Id, addedEventName);
            Sut.Execute(request);

            Assert.AreEqual(addedEventName, Deps.Location.Added.Name);
        }

        [Test]
        public void AddEvent_InvalidInput_ThrowsValidationException()
        {
            Deps.Location.ThrowOnAdd = true;

            const string addedEventName = "";

            var request = new AddLocation.Request(TestData.BunchA.Id, addedEventName);

            Assert.Throws<ValidationException>(() => Sut.Execute(request));
        }

        private AddLocation Sut => new AddLocation(
            Deps.Location);
    }
}