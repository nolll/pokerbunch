using System;
using Application.UseCases.CashgameContext;
using Application.UseCases.CashgameTopList;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Toplist;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.CashgameModelFactories.Toplist
{
    public class CashgameToplistPageBuilder : ICashgameToplistPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly ICashgameToplistTableModelFactory _cashgameToplistTableModelFactory;
        private readonly ICashgamePageNavigationModelFactory _cashgamePageNavigationModelFactory;
        private readonly ICashgameYearNavigationModelFactory _cashgameYearNavigationModelFactory;
        private readonly ICashgameTopListInteractor _cashgameTopListInteractor;
        private readonly ICashgameContextInteractor _cashgameContextInteractor;

        public CashgameToplistPageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            ICashgameToplistTableModelFactory cashgameToplistTableModelFactory,
            ICashgamePageNavigationModelFactory cashgamePageNavigationModelFactory,
            ICashgameYearNavigationModelFactory cashgameYearNavigationModelFactory,
            ICashgameTopListInteractor cashgameTopListInteractor,
            ICashgameContextInteractor cashgameContextInteractor)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _cashgameToplistTableModelFactory = cashgameToplistTableModelFactory;
            _cashgamePageNavigationModelFactory = cashgamePageNavigationModelFactory;
            _cashgameYearNavigationModelFactory = cashgameYearNavigationModelFactory;
            _cashgameTopListInteractor = cashgameTopListInteractor;
            _cashgameContextInteractor = cashgameContextInteractor;
        }

        public CashgameToplistPageModel Build(string slug, string sortOrderParam, int? year)
        {
            var contextResult = GetCashgameContextResult(slug, year);
            var topListResult = GetTopListResult(slug, sortOrderParam, year);
            return Build(contextResult, topListResult);
        }

        private CashgameToplistPageModel Build(CashgameContextResult contextResult, CashgameTopListResult topListResult)
        {
            var pageProperties = _pagePropertiesFactory.Create(contextResult);
            var pageNavModel = _cashgamePageNavigationModelFactory.Create(contextResult, CashgamePage.Toplist);
            var yearNavModel = _cashgameYearNavigationModelFactory.Create(contextResult, CashgamePage.Toplist);
            var tableModel = _cashgameToplistTableModelFactory.Create(topListResult);

            return new CashgameToplistPageModel
            {
                BrowserTitle = "Cashgame Toplist",
                PageProperties = pageProperties,
                TableModel = tableModel,
                PageNavModel = pageNavModel,
                YearNavModel = yearNavModel
            };
        }

        private CashgameContextResult GetCashgameContextResult(string slug, int? year)
        {
            var contextRequest = new CashgameContextRequest
                {
                    Slug = slug,
                    Year = year
                };
            var contextResult = _cashgameContextInteractor.Execute(contextRequest);
            return contextResult;
        }

        private CashgameTopListResult GetTopListResult(string slug, string sortOrderParam, int? year)
        {
            var topListRequest = new CashgameTopListRequest
                {
                    Slug = slug,
                    OrderBy = GetToplistSortOrder(sortOrderParam),
                    Year = year
                };
            var topListResult = _cashgameTopListInteractor.Execute(topListRequest);
            return topListResult;
        }

        private ToplistSortOrder GetToplistSortOrder(string s)
        {
            if (s == null)
            {
                return ToplistSortOrder.Winnings;
            }
            ToplistSortOrder sortOrder;
            if (Enum.TryParse(s, out sortOrder))
            {
                return sortOrder;
            }
            return ToplistSortOrder.Winnings;
        }

    }
}