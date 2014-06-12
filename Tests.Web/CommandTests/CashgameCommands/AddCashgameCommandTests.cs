using Application.Factories;
using Core.Entities;
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
        public void Execute_WithoutLocation_ReturnsFalse()
        {
            var homegame = new HomegameInTest();
            var model = new AddCashgamePostModel();

            var sut = GetSut(homegame, model);
            var result = sut.Execute();

            Assert.IsFalse(result);
        }

        [Test]
        public void Execute_WithTypedLocation_ReturnsTrue()
        {
            var homegame = new HomegameInTest();
            var model = new AddCashgamePostModel { TypedLocation = "a" };

            var sut = GetSut(homegame, model);
            var result = sut.Execute();

            Assert.IsTrue(result);
        }

        [Test]
        public void Execute_WithSelectedLocation_ReturnsTrue()
        {
            var homegame = new HomegameInTest();
            var model = new AddCashgamePostModel { SelectedLocation = "a" };

            var sut = GetSut(homegame, model);
            var result = sut.Execute();

            Assert.IsTrue(result);
        }

        [Test]
        public void Execute_WithvalidModel_CallsAddGame()
        {
            const int homegameId = 2;
            var cashgame = new CashgameInTest(id: 1);
            var homegame = new HomegameInTest(id: homegameId);
            const string location = "a";
            var model = new AddCashgamePostModel { TypedLocation = location };

            GetMock<ICashgameFactory>().Setup(o => o.Create(location, homegameId, (int)GameStatus.Running, null, null)).Returns(cashgame);

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
