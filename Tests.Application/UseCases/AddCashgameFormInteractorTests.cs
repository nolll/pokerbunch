using Application.Exceptions;
using Application.UseCases.CashgameOptions;
using Core.Entities;
using Core.Repositories;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class AddCashgameFormInteractorTests : MockContainer
    {
        [Test]
        public void Execute_ReturnsResultObject()
        {
            const string slug = "a";
            var result = Sut.Execute(new CashgameOptionsRequest(slug));

            Assert.IsInstanceOf<CashgameOptionsResult>(result);
        }

        [Test]
        public void Execute_WithRunningCashgame_ThrowsException()
        {
            const string slug = "a";

            GetMock<ICashgameRepository>().Setup(o => o.GetRunning(It.IsAny<Homegame>())).Returns(new CashgameInTest());

            Assert.Throws<CashgameRunningException>(() => Sut.Execute(new CashgameOptionsRequest(slug)));
        }

        private CashgameOptionsInteractor Sut
        {
            get
            {
                return new CashgameOptionsInteractor(
                    GetMock<IHomegameRepository>().Object,
                    GetMock<ICashgameRepository>().Object);
            }
        }
    }
}