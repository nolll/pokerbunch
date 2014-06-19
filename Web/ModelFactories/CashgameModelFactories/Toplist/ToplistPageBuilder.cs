using System;
using Application.UseCases.CashgameContext;
using Application.UseCases.CashgameTopList;
using Web.Models.CashgameModels.Toplist;

namespace Web.ModelFactories.CashgameModelFactories.Toplist
{
    public class ToplistPageBuilder : IToplistPageBuilder
    {
        private readonly ITopListInteractor _topListInteractor;
        private readonly ICashgameContextInteractor _contextInteractor;

        public ToplistPageBuilder(
            ITopListInteractor topListInteractor,
            ICashgameContextInteractor contextInteractor)
        {
            _topListInteractor = topListInteractor;
            _contextInteractor = contextInteractor;
        }

        public CashgameToplistPageModel Build(string slug, string sortOrderParam, int? year)
        {
            var contextResult = _contextInteractor.Execute(GetCashgameContextRequest(slug, year));
            var topListResult = _topListInteractor.Execute(GetTopListRequest(slug, sortOrderParam, year));

            return new CashgameToplistPageModel(
                contextResult,
                topListResult);
        }

        private static CashgameContextRequest GetCashgameContextRequest(string slug, int? year)
        {
            var contextRequest = new CashgameContextRequest
                {
                    Slug = slug,
                    Year = year
                };
            return contextRequest;
        }

        private TopListRequest GetTopListRequest(string slug, string sortOrderParam, int? year)
        {
            var topListRequest = new TopListRequest
                {
                    Slug = slug,
                    OrderBy = ParseToplistSortOrder(sortOrderParam),
                    Year = year
                };
            return topListRequest;
        }

        private ToplistSortOrder ParseToplistSortOrder(string s)
        {
            if (s == null)
                return ToplistSortOrder.Winnings;
            ToplistSortOrder sortOrder;
            return Enum.TryParse(s, true, out sortOrder) ? sortOrder : ToplistSortOrder.Winnings;
        }
    }
}