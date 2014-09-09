using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Application.UseCases
{
    class CashgameListTests : MockContainer
    {
        private const string Slug = "a";

        [Test]
        public void CashgameList_HasEmptyListOfGames()
        {
            var bunch = A.Bunch.Build();
            var cashgames = new List<Cashgame>();
            GetMock<IBunchRepository>().Setup(o => o.GetBySlug(Slug)).Returns(bunch);
            GetMock<ICashgameRepository>().Setup(o => o.GetPublished(It.IsAny<Bunch>(), null)).Returns(cashgames);

            var result = Execute(CreateRequest());

            Assert.AreEqual(0, result.List.Count);
        }

        [Test]
        public void CashgameList_WithOneGame_LocationIsSet()
        {
            var bunch = A.Bunch.Build();
            var cashgame = A.Cashgame.Build();
            var cashgames = new List<Cashgame> { cashgame };
            GetMock<IBunchRepository>().Setup(o => o.GetBySlug(Slug)).Returns(bunch);
            GetMock<ICashgameRepository>().Setup(o => o.GetPublished(It.IsAny<Bunch>(), null)).Returns(cashgames);

            var result = Execute(CreateRequest());

            Assert.AreEqual("Location", result.List[0].Location);
        }

        private CashgameListRequest CreateRequest()
        {
            return new CashgameListRequest(Slug);
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
            var list = cashgames.Select(o => new CashgameItem(o)).ToList();

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

        public CashgameItem(Cashgame cashgame)
        {
            Location = cashgame.Location;
        }
    }
}
