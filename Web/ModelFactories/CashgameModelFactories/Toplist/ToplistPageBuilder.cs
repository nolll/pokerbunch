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
        private readonly ICashgamePageNavigationModelFactory _cashgamePageNavigationModelFactory;
        private readonly ICashgameYearNavigationModelFactory _cashgameYearNavigationModelFactory;
        private readonly ITopListInteractor _topListInteractor;
        private readonly ICashgameContextInteractor _cashgameContextInteractor;

        public ToplistPageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            ICashgamePageNavigationModelFactory cashgamePageNavigationModelFactory,
            ICashgameYearNavigationModelFactory cashgameYearNavigationModelFactory,
            ITopListInteractor topListInteractor,
            ICashgameContextInteractor cashgameContextInteractor)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _cashgamePageNavigationModelFactory = cashgamePageNavigationModelFactory;
            _cashgameYearNavigationModelFactory = cashgameYearNavigationModelFactory;
            _topListInteractor = topListInteractor;
            _cashgameContextInteractor = cashgameContextInteractor;
        }

        public CashgameToplistPageModel Build(string slug, string sortOrderParam, int? year)
        {
            var contextResult = GetCashgameContext(slug, year);
            var topListResult = GetTopList(slug, sortOrderParam, year);
            return Build(contextResult, topListResult);
        }

        private CashgameToplistPageModel Build(CashgameContextResult contextResult, TopListResult topListResult)
        {
            var pageProperties = _pagePropertiesFactory.Create(contextResult);
            var pageNavModel = _cashgamePageNavigationModelFactory.Create(contextResult, CashgamePage.Toplist);
            var yearNavModel = _cashgameYearNavigationModelFactory.Create(contextResult, CashgamePage.Toplist);
            var tableModel = new ToplistTableModel(topListResult);

            return new CashgameToplistPageModel
            {
                BrowserTitle = "Cashgame Toplist",
                PageProperties = pageProperties,
                TableModel = tableModel,
                PageNavModel = pageNavModel,
                YearNavModel = yearNavModel
            };
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