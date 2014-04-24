using System.Collections.Generic;
using Application.UseCases.BunchList;
using Core.Classes;
using Core.Repositories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    public class BunchListInteractorTests : MockContainer
    {
        private BunchListInteractor _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new BunchListInteractor(
                GetMock<IHomegameRepository>().Object);
        }

        [Test]
        public void Execute_WithBunches_ReturnsListOfBunchItems()
        {
            const string displayName = "a";
            const string slug = "b";
            var homegame = new FakeHomegame(displayName: displayName, slug: slug);
            var homegames = new List<Homegame>{homegame};
            GetMock<IHomegameRepository>().Setup(o => o.GetList()).Returns(homegames);

            var result = _sut.Execute();

            Assert.AreEqual(1, result.Bunches.Count);
            Assert.AreEqual(displayName, result.Bunches[0].DisplayName);
            Assert.AreEqual(slug, result.Bunches[0].Slug);
        }
    }
}
