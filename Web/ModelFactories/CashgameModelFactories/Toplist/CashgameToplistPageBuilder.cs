using System;
using Core.Repositories;
using Core.UseCases.CashgameTopList;
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
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly ICashgameTopListInteractor _cashgameTopListInteractor;

        public CashgameToplistPageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            ICashgameToplistTableModelFactory cashgameToplistTableModelFactory,
            ICashgamePageNavigationModelFactory cashgamePageNavigationModelFactory,
            ICashgameYearNavigationModelFactory cashgameYearNavigationModelFactory,
            IHomegameRepository homegameRepository,
            ICashgameRepository cashgameRepository,
            ICashgameTopListInteractor cashgameTopListInteractor)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _cashgameToplistTableModelFactory = cashgameToplistTableModelFactory;
            _cashgamePageNavigationModelFactory = cashgamePageNavigationModelFactory;
            _cashgameYearNavigationModelFactory = cashgameYearNavigationModelFactory;
            _homegameRepository = homegameRepository;
            _cashgameRepository = cashgameRepository;
            _cashgameTopListInteractor = cashgameTopListInteractor;
        }

        public CashgameToplistPageModel Build(string slug, string sortOrderParam, int? year)
        {
            var request = new CashgameTopListRequest
                {
                    Slug = slug,
                    OrderBy = GetToplistSortOrder(sortOrderParam),
                    Year = year
                };
            var result = _cashgameTopListInteractor.Execute(request);
            var homegame = _homegameRepository.GetBySlug(slug);
            var years = _cashgameRepository.GetYears(homegame);
            var pageProperties = _pagePropertiesFactory.Create(homegame);
            var tableModel = _cashgameToplistTableModelFactory.Create(result);
            var pageNavModel = _cashgamePageNavigationModelFactory.Create(homegame.Slug, CashgamePage.Toplist);
            var yearNavModel = _cashgameYearNavigationModelFactory.Create(homegame, years, CashgamePage.Toplist, year);

            return new CashgameToplistPageModel
                {
                    BrowserTitle = "Cashgame Toplist",
                    PageProperties = pageProperties,
			        TableModel = tableModel,
                    PageNavModel = pageNavModel,
                    YearNavModel = yearNavModel
                };
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