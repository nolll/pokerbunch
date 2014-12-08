using System.Linq;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Urls;
using Core.UseCases.AddCashgame;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class AddCashgameTests : TestBase
    {
        private const string Location = "b";

        [Test]
        public void AddCashgame_ReturnUrlIsSet()
        {
            var request = CreateRequest();
            var result = Execute(request);

            Assert.IsInstanceOf<RunningCashgameUrl>(result.ReturnUrl);
        }

        [Test]
        public void AddCashgame_WithLocation_GameIsAdded()
        {
            var request = CreateRequest();
            Execute(request);

            GetMock<ICashgameRepository>().Verify(o => o.AddGame(It.IsAny<Bunch>(), It.IsAny<Cashgame>()));
        }

        [Test]
        public void AddCashgame_WithoutLocation_ThrowsValidationException()
        {
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
            return new AddCashgameRequest(Constants.SlugA, location);
        }

        private AddCashgameResult Execute(AddCashgameRequest request)
        {
            return AddCashgameInteractor.Execute(Repos.Bunch, GetMock<ICashgameRepository>().Object, request);
        }
    }
}
