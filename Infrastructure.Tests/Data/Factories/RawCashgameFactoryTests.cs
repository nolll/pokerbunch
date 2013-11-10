using Core.Classes;
using Infrastructure.Data.Factories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Infrastructure.Tests.Data.Factories
{
    public class RawCashgameFactoryTests : InfrastructureMockContainer
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
            var result = sut.Create(new FakeCashgame (status: GameStatus.Created), GameStatus.Published);

            Assert.AreEqual(result.Status, (int)GameStatus.Published);
        }

        private RawCashgameFactory GetSut()
        {
            return new RawCashgameFactory(
                Mocks.TimeProviderMock.Object);
        }
    }
}
