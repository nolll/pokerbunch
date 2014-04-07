using Application.Factories;
using Core.Repositories;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.Commands.CashgameCommands;
using Web.ModelMappers;
using Web.Models.CashgameModels.Add;
using Web.Models.CashgameModels.Buyin;
using Web.Models.CashgameModels.Cashout;
using Web.Models.CashgameModels.Edit;
using Web.Models.CashgameModels.Report;

namespace Tests.Web.CommandTests.CashgameCommands
{
    public class CashgameCommandProviderTests : MockContainer
    {
        [Test]
        public void GetEndGameCommand_ReturnsEndGameCommand()
        {
            var sut = GetSut();
            var result = sut.GetEndGameCommand(It.IsAny<string>());

            Assert.IsInstanceOf<EndGameCommand>(result);
        }

        [Test]
        public void GetAddCommand_ReturnsAddCashgameCommand()
        {
            var sut = GetSut();
            var result = sut.GetAddCommand(It.IsAny<string>(), It.IsAny<AddCashgamePostModel>());

            Assert.IsInstanceOf<AddCashgameCommand>(result);
        }

        [Test]
        public void GetEditCommand_ReturnsEditCashgameCommand()
        {
            var sut = GetSut();
            var result = sut.GetEditCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CashgameEditPostModel>());

            Assert.IsInstanceOf<EditCashgameCommand>(result);
        }

        [Test]
        public void GetBuyinCommand_ReturnsBuyinCommand()
        {
            var sut = GetSut();
            var result = sut.GetBuyinCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<BuyinPostModel>());

            Assert.IsInstanceOf<BuyinCommand>(result);
        }

        [Test]
        public void GetReportCommand_ReturnsReportCommand()
        {
            var sut = GetSut();
            var result = sut.GetReportCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ReportPostModel>());

            Assert.IsInstanceOf<ReportCommand>(result);
        }

        [Test]
        public void GetDeleteCheckpointCommand_ReturnsDeleteCheckpointCommand()
        {
            var sut = GetSut();
            var result = sut.GetDeleteCheckpointCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>());

            Assert.IsInstanceOf<DeleteCheckpointCommand>(result);
        }

        [Test]
        public void GetCashoutCommand_ReturnsCashoutCommand()
        {
            const string slug = "a";
            const int playerId = 1;
            const string playerName = "b";
            var homegame = new FakeHomegame();
            var player = new FakePlayer(playerId, displayName: playerName);
            var cashgame = new FakeCashgame();

            GetMock<IHomegameRepository>().Setup(o => o.GetBySlug(slug)).Returns(homegame);
            GetMock<IPlayerRepository>().Setup(o => o.GetByName(homegame, playerName)).Returns(player);
            GetMock<ICashgameRepository>().Setup(o => o.GetRunning(homegame)).Returns(cashgame);

            var sut = GetSut();
            var result = sut.GetCashoutCommand(slug, playerName, It.IsAny<CashoutPostModel>());

            Assert.IsInstanceOf<CashoutCommand>(result);
        }

        [Test]
        public void GetDeleteCommand_ReturnsDeleteCommand()
        {
            var sut = GetSut();
            var result = sut.GetDeleteCommand(It.IsAny<string>(), It.IsAny<string>());

            Assert.IsInstanceOf<DeleteCommand>(result);
        }

        private CashgameCommandProvider GetSut()
        {
            return new CashgameCommandProvider(
                GetMock<IHomegameRepository>().Object,
                GetMock<ICashgameRepository>().Object,
                GetMock<ICashgameFactory>().Object,
                GetMock<ICashgameModelMapper>().Object,
                GetMock<IPlayerRepository>().Object,
                GetMock<ICheckpointModelMapper>().Object,
                GetMock<ICheckpointRepository>().Object);
        }
    }
}
