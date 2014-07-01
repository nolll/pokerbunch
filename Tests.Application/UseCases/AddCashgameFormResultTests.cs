using System.Collections.Generic;
using Application.UseCases.CashgameOptions;
using NUnit.Framework;

namespace Tests.Application.UseCases
{
    class AddCashgameFormResultTests
    {
        [Test]
        public void Construct_LocationsAreSet()
        {
            const string location1 = "a";
            const string location2 = "b";
            var locations = new List<string> { location1, location2 };

            var result = new AddCashgameFormResult(locations);

            Assert.AreEqual(2, result.Locations.Count);
            Assert.AreEqual(location1, result.Locations[0]);
            Assert.AreEqual(location2, result.Locations[1]);
        }
    }
}