using System.Linq;
using Application.Factories;
using Application.UseCases.AddCashgame;
using Core.Repositories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class AddCashgameTests : MockContainer
    {
        private const string Slug = "a";
        private const string Location = "b";

        [Test]
        public void AddCashgame_WithLocation_CashgameIsAdded()
        {
            SetupHomegame();

            var request = CreateRequest();
            var result = Sut.Execute(request);

            Assert.IsTrue(result.CreatedGame);
        }

        [Test]
        public void AddCashgame_WithoutLocation_CashgameIsAdded()
        {
            SetupHomegame();

            var request = CreateRequestWithoutLocation();
            var result = Sut.Execute(request);

            Assert.IsFalse(result.CreatedGame);
            Assert.AreEqual(1, result.Errors.Count());
        }

        private static AddCashgameRequest CreateRequestWithoutLocation()
        {
            return CreateRequest(null);
        }

        private static AddCashgameRequest CreateRequest(string location = Location)
        {
            return new AddCashgameRequest(Slug, location);
        }

        private void SetupHomegame()
        {
            var homegame = new HomegameInTest();
            GetMock<IHomegameRepository>().Setup(o => o.GetBySlug(Slug)).Returns(homegame);
        }

        private AddCashgameInteractor Sut
        {
            get
            {
                return new AddCashgameInteractor(
                    GetMock<IHomegameRepository>().Object,
                    GetMock<ICashgameRepository>().Object);
            }
        }
    }
}
