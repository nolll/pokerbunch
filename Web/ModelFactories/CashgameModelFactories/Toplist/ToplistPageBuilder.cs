using System;
using Application.UseCases.ApplicationContext;
using Application.UseCases.CashgameContext;
using Application.UseCases.CashgameTopList;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Toplist;

namespace Web.ModelFactories.CashgameModelFactories.Toplist
{
    public class ToplistPageBuilder : IToplistPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly ITopListInteractor _topListInteractor;
        private readonly ICashgameContextInteractor _cashgameContextInteractor;
        private readonly IApplicationContextInteractor _applicationContextInteractor;

        public ToplistPageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            ITopListInteractor topListInteractor,
            ICashgameContextInteractor cashgameContextInteractor,
            IApplicationContextInteractor applicationContextInteractor)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _topListInteractor = topListInteractor;
            _cashgameContextInteractor = cashgameContextInteractor;
            _applicationContextInteractor = applicationContextInteractor;
        }

        public CashgameToplistPageModel Build(string slug, string sortOrderParam, int? year)
        {
            var applicationContextResult = _applicationContextInteractor.Execute();
            var cashgameContextResult = GetCashgameContext(slug, year);
            var topListResult = GetTopList(slug, sortOrderParam, year);

            return new CashgameToplistPageModel(
                applicationContextResult,
                cashgameContextResult,
                topListResult);
        }

        private CashgameContextResult GetCashgameContext(string slug, int? year)
        {
            var contextRequest = new CashgameContextRequest
                {
                    Slug = slug,
                    Year = year
                };
            return _cashgameContextInteractor.Execute(contextRequest);
        }

        private TopListResult GetTopList(string slug, string sortOrderParam, int? year)
        {
            var topListRequest = new TopListRequest
                {
                    Slug = slug,
                    OrderBy = GetToplistSortOrder(sortOrderParam),
                    Year = year
                };
            return _topListInteractor.Execute(topListRequest);
        }

        private ToplistSortOrder GetToplistSortOrder(string s)
        {
            if (s == null)
                return ToplistSortOrder.Winnings;
            ToplistSortOrder sortOrder;
            return Enum.TryParse(s, true, out sortOrder) ? sortOrder : ToplistSortOrder.Winnings;
        }
    }
}