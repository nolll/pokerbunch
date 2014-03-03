using Application.Factories;
using Core.Classes;
using Core.Repositories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.Commands.CashgameCommands;
using Web.Models.CashgameModels.Add;

namespace Tests.Web.CommandTests.CashgameCommands
{
    public class AddCashgameCommandTests : MockContainer
    {
        [Test]
        public void Execute_WithInvalidModel_ReturnsFalse()
        {
            var homegame = new FakeHomegame();
            var model = new AddCashgamePostModel();

            var sut = GetSut(homegame, model);
            var result = sut.Execute();

            Assert.IsFalse(result);
        }

        [Test]
        public void Execute_WithvalidModel_ReturnsTrue()
        {
            var homegame = new FakeHomegame();
            var model = new AddCashgamePostModel { TypedLocation = "a" };

            var sut = GetSut(homegame, model);
            var result = sut.Execute();

            Assert.IsTrue(result);
        }

        [Test]
        public void Execute_WithvalidModel_Calls ()
        {
            var cashgame = new FakeCashgame(id: 1);
            var homegame = new FakeHomegame();
            const string location = "a";
            var model = new AddCashgamePostModel { TypedLocation = location };

            GetMock<ICashgameFactory>().Setup(o => o.Create(location, (int)GameStatus.Running, null, null)).Returns(cashgame);

            var sut = GetSut(homegame, model);
            sut.Execute();

            GetMock<ICashgameRepository>().Verify(o => o.AddGame(homegame, cashgame));
        }

        private AddCashgameCommand GetSut(Homegame homegame, AddCashgamePostModel model)
        {
            return new AddCashgameCommand(
                GetMock<ICashgameRepository>().Object,
                GetMock<ICashgameFactory>().Object,
                homegame,
                model);
        }
    }
}
