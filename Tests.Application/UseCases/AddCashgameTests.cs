using System.Linq;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Urls;
using Core.UseCases.AddCashgame;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class AddCashgameTests : TestBase
    {
        private const string Slug = "a";
        private const string Location = "b";

        [Test]
        public void AddCashgame_ReturnUrlIsSet()
        {
            SetupHomegame();

            var request = CreateRequest();
            var result = Execute(request);

            Assert.IsInstanceOf<RunningCashgameUrl>(result.ReturnUrl);
        }

        [Test]
        public void AddCashgame_WithLocation_GameIsAdded()
        {
            SetupHomegame();

            var request = CreateRequest();
            Execute(request);

            GetMock<ICashgameRepository>().Verify(o => o.AddGame(It.IsAny<Bunch>(), It.IsAny<Cashgame>()));
        }

        [Test]
        public void AddCashgame_WithoutLocation_ThrowsValidationException()
        {
            SetupHomegame();

            var request = CreateRequestWithoutLocation();

            var ex = Assert.Throws<ValidationException>(() => Execute(request));
            Assert.AreEqual(1, ex.Messages.Count());
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
            var homegame = new BunchInTest();
            GetMock<IBunchRepository>().Setup(o => o.GetBySlug(Slug)).Returns(homegame);
        }
        
        private AddCashgameResult Execute(AddCashgameRequest request)
        {
            return AddCashgameInteractor.Execute(GetMock<IBunchRepository>().Object, GetMock<ICashgameRepository>().Object, request);
        }
    }
}
