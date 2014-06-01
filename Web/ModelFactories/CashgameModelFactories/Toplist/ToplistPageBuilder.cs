using System;
using Application.UseCases.CashgameContext;
using Application.UseCases.CashgameTopList;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Toplist;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.CashgameModelFactories.Toplist
{
    public class ToplistPageBuilder : IToplistPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly ITopListInteractor _topListInteractor;
        private readonly ICashgameContextInteractor _cashgameContextInteractor;

        public ToplistPageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            ITopListInteractor topListInteractor,
            ICashgameContextInteractor cashgameContextInteractor)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _topListInteractor = topListInteractor;
            _cashgameContextInteractor = cashgameContextInteractor;
        }

        public CashgameToplistPageModel Build(string slug, string sortOrderParam, int? year)
        {
            var applicationContextResult = GetApplicationContext();
            var cashgameContextResult = GetCashgameContext(slug, year);
            var topListResult = GetTopList(slug, sortOrderParam, year);

            var pageProperties = _pagePropertiesFactory.Create(cashgameContextResult);

            return new CashgameToplistPageModel(
                pageProperties,
                cashgameContextResult,
                topListResult);
        }

        private object GetApplicationContext()
        {
            return new object();
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