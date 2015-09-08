using System.Linq;
using Core.Exceptions;
using Core.UseCases;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class AddCashgameTests : TestBase
    {
        [Test]
        public void AddCashgame_SlugIsSet()
        {
            var request = CreateRequest();
            var result = Sut.Execute(request);

            Assert.AreEqual(TestData.SlugA, result.Slug);
        }

        [Test]
        public void AddCashgame_WithLocation_GameIsAdded()
        {
            var request = CreateRequest();
            Sut.Execute(request);

            Assert.IsNotNull(Repos.Cashgame.Added);
        }

        [Test]
        public void AddCashgame_WithoutLocation_ThrowsValidationException()
        {
            var request = CreateRequestWithoutLocation();

            var ex = Assert.Throws<ValidationException>(() => Sut.Execute(request));
            Assert.AreEqual(1, ex.Messages.Count());
        }

        private static AddCashgame.Request CreateRequestWithoutLocation()
        {
            return CreateRequest(null);
        }

        private static AddCashgame.Request CreateRequest(string location = TestData.LocationA)
        {
            return new AddCashgame.Request(TestData.UserNameA, TestData.SlugA, location);
        }

        private AddCashgame Sut
        {
            get
            {
                return new AddCashgame(
                    Repos.Bunch,
                    Repos.Cashgame,
                    Services.UserService,
                    Repos.Player);
            }
        }
    }
}
