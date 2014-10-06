using System.Collections.Generic;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.UseCases.AddCashgameForm;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Core.UseCases
{
    class AddCashgameFormTests : TestBase
    {
        [Test]
        public void AddCashgameOptions_ReturnsResultObject()
        {
            const string slug = "a";
            var result = Execute(new AddCashgameFormRequest(slug));

            Assert.IsInstanceOf<AddCashgameFormResult>(result);
        }

        [Test]
        public void AddCashgameOptions_WithRunningCashgame_ThrowsException()
        {
            const string slug = "a";

            GetMock<ICashgameRepository>().Setup(o => o.GetRunning(It.IsAny<Bunch>())).Returns(new CashgameInTest());

            Assert.Throws<CashgameRunningException>(() => Execute(new AddCashgameFormRequest(slug)));
        }

        [Test]
        public void AddCashgameOptions_LocationsAreSet()
        {
            const string location1 = "a";
            const string location2 = "b";
            var locations = new List<string> { location1, location2 };

            GetMock<ICashgameRepository>().Setup(o => o.GetLocations(It.IsAny<Bunch>())).Returns(locations);

            const string slug = "a";
            var result = Execute(new AddCashgameFormRequest(slug));

            Assert.AreEqual(2, result.Locations.Count);
            Assert.AreEqual(location1, result.Locations[0]);
            Assert.AreEqual(location2, result.Locations[1]);
        }

        private AddCashgameFormResult Execute(AddCashgameFormRequest request)
        {
            return AddCashgameFormInteractor.Execute(
                GetMock<IBunchRepository>().Object,
                GetMock<ICashgameRepository>().Object,
                request);
        }
    }
}