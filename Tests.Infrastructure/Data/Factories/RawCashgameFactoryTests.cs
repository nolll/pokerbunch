using Application.Services;
using Core.Entities;
using Infrastructure.Data.Factories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Infrastructure.Data.Factories
{
    public class RawCashgameFactoryTests : MockContainer
    {
        [Test]
        public void Create_WithoutStatus_StatusIsSetFromCashgame()
        {
            var sut = GetSut();
            var result = sut.Create(new FakeCashgame (status: GameStatus.Published));

            Assert.AreEqual(result.Status, (int)GameStatus.Published);
        }

        [Test]
        public void Create_WithStatus_StatusIsSet()
        {
            var sut = GetSut();
            var result = sut.Create(new FakeCashgame (), GameStatus.Published);

            Assert.AreEqual(result.Status, (int)GameStatus.Published);
        }

        private RawCashgameFactory GetSut()
        {
            return new RawCashgameFactory(
                GetMock<ITimeProvider>().Object);
        }
    }
}
