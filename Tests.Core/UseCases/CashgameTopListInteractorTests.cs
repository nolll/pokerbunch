using System;
using Core.Repositories;
using Core.Services.Interfaces;
using Core.UseCases.CashgameTopList;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Core.UseCases
{
    class CashgameTopListInteractorTests : MockContainer
    {
        private CashgameTopListInteractor _sut;

        [SetUp]
        public virtual void SetUp()
        {
            _sut = new CashgameTopListInteractor(
                GetMock<IHomegameRepository>().Object,
                GetMock<ICashgameService>().Object);
        }

        [Test]
        public void Execute_NoSlug_ThrowsException()
        {
            var request = new CashgameTopListRequest();

            var ex = Assert.Throws<ArgumentException>(() => _sut.Execute(request));
        }

        [Test]
        public void Execute_WithSlug_ReturnsTopListItems()
        {
            const string slug = "a";
            var homegame = new FakeHomegame();
            var suite = new FakeCashgameSuite();
            var request = new CashgameTopListRequest{Slug = slug};

            GetMock<IHomegameRepository>().Setup(o => o.GetBySlug(slug)).Returns(homegame);
            GetMock<ICashgameService>().Setup(o => o.GetSuite(homegame, null)).Returns(suite);

            var result = _sut.Execute(request);

            Assert.AreEqual(1, result.Items.Count);
            Assert.AreEqual("", result.Items[0].Name);
            Assert.AreEqual(0, result.Items[0].Winnings);
            Assert.AreEqual(0, result.Items[0].Buyin);
            Assert.AreEqual(0, result.Items[0].Cashout);
            Assert.AreEqual(0, result.Items[0].MinutesPlayed);
            Assert.AreEqual(0, result.Items[0].GamesPlayed);
            Assert.AreEqual(0, result.Items[0].WinRate);
        }
    }
}
