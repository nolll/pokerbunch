using Core.Entities;
using Core.Services.Interfaces;
using Infrastructure.Data.Factories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Infrastructure.Data.Factories
{
    public class RawCashgameFactoryTests : TestBase
    {
        [Test]
        public void Create_WithoutStatus_StatusIsSetFromCashgame()
        {
            var sut = GetSut();
            var result = sut.Create(new CashgameInTest (status: GameStatus.Finished));

            Assert.AreEqual(result.Status, (int)GameStatus.Finished);
        }

        [Test]
        public void Create_WithStatus_StatusIsSet()
        {
            var sut = GetSut();
            var result = sut.Create(new CashgameInTest(), GameStatus.Finished);

            Assert.AreEqual(result.Status, (int)GameStatus.Finished);
        }

        private RawCashgameFactory GetSut()
        {
            return new RawCashgameFactory(
                GetMock<ITimeProvider>().Object);
        }
    }
}
