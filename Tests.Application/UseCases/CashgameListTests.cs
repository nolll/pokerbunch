using System.Collections.Generic;
using System.Linq;
using Application.Urls;
using Core.Entities;
using Core.Repositories;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Application.UseCases
{
    class CashgameListTests : TestBase
    {
        private const string Slug = "a";

        [Test]
        public void CashgameList_HasEmptyListOfGames()
        {
            var bunch = A.Bunch.Build();
            var cashgames = new List<Cashgame>();
            SetupBunch(bunch);
            SetupCashgame(cashgames);

            var result = Execute(CreateRequest());

            Assert.AreEqual(0, result.List.Count);
        }

        [Test]
        public void CashgameList_WithOneGame_LocationIsSet()
        {
            var bunch = A.Bunch.Build();
            var cashgame = A.Cashgame.Build();
            var cashgames = new List<Cashgame> { cashgame };
            SetupBunch(bunch);
            SetupCashgame(cashgames);

            var result = Execute(CreateRequest());

            Assert.AreEqual("Location", result.List[0].Location);
        }

        [Test]
        public void CashgameList_WithOneGame_UrlIsSet()
        {
            var bunch = A.Bunch.Build();
            var cashgame = A.Cashgame.Build();
            var cashgames = new List<Cashgame> { cashgame };
            SetupBunch(bunch);
            SetupCashgame(cashgames);

            var result = Execute(CreateRequest());

            Assert.AreEqual("/a/cashgame/details/2001-01-01", result.List[0].Url.Relative);
        }

        [Test]
        public void CashgameList_WithOneGame_DurationIsSet()
        {
            var bunch = A.Bunch.Build();
            var cashgame = A.Cashgame.Build();
            var cashgames = new List<Cashgame> { cashgame };
            SetupBunch(bunch);
            SetupCashgame(cashgames);

            var result = Execute(CreateRequest());

            Assert.AreEqual(1, result.List[0].Duration);
        }

        private CashgameListRequest CreateRequest()
        {
            return new CashgameListRequest(Slug);
        }

        private void SetupBunch(Bunch bunch)
        {
            GetMock<IBunchRepository>().Setup(o => o.GetBySlug(Slug)).Returns(bunch);
        }

        private void SetupCashgame(IList<Cashgame> cashgames)
        {
            GetMock<ICashgameRepository>().Setup(o => o.GetPublished(It.IsAny<Bunch>(), null)).Returns(cashgames);
        }

        private CashgameListResult Execute(CashgameListRequest request)
        {
            return CashgameListInteractor.Execute(
                GetMock<IBunchRepository>().Object,
                GetMock<ICashgameRepository>().Object,
                request);
        }
    }

    public class CashgameListInteractor
    {
        public static CashgameListResult Execute(
            IBunchRepository bunchRepository,
            ICashgameRepository cashgameRepository,
            CashgameListRequest request)
        {
            var bunch = bunchRepository.GetBySlug(request.Slug);
            var cashgames = cashgameRepository.GetPublished(bunch, null);
            var list = cashgames.Select(o => new CashgameItem(bunch.Slug, o)).ToList();

            return new CashgameListResult(list);
        }
    }

    public class CashgameListRequest
    {
        public string Slug { get; private set; }

        public CashgameListRequest(string slug)
        {
            Slug = slug;
        }
    }

    public class CashgameListResult
    {
        public IList<CashgameItem> List { get; private set; }

        public CashgameListResult(IList<CashgameItem> list)
        {
            List = list;
        }
    }

    public class CashgameItem
    {
        public string Location { get; private set; }
        public Url Url { get; private set; }
        public int Duration { get; private set; }

        public CashgameItem(string slug, Cashgame cashgame)
        {
            Location = cashgame.Location;
            Url = new CashgameDetailsUrl(slug, cashgame.DateString);
            Duration = cashgame.Duration;
        }
    }
}
