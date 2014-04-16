using System;
using Application.Services;
using Core.Repositories;
using Core.Services.Interfaces;
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
        private readonly ICashgameService _cashgameService;
        private readonly ICashgameTopListInteractor _cashgameTopListInteractor;

        public CashgameToplistPageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            ICashgameToplistTableModelFactory cashgameToplistTableModelFactory,
            ICashgamePageNavigationModelFactory cashgamePageNavigationModelFactory,
            ICashgameYearNavigationModelFactory cashgameYearNavigationModelFactory,
            IHomegameRepository homegameRepository,
            ICashgameRepository cashgameRepository,
            ICashgameService cashgameService,
            ICashgameTopListInteractor cashgameTopListInteractor)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _cashgameToplistTableModelFactory = cashgameToplistTableModelFactory;
            _cashgamePageNavigationModelFactory = cashgamePageNavigationModelFactory;
            _cashgameYearNavigationModelFactory = cashgameYearNavigationModelFactory;
            _homegameRepository = homegameRepository;
            _cashgameRepository = cashgameRepository;
            _cashgameService = cashgameService;
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
            _cashgameTopListInteractor.Execute(request);
            var homegame = _homegameRepository.GetBySlug(slug);
            var suite = _cashgameService.GetSuite(homegame, year);
            var years = _cashgameRepository.GetYears(homegame);
            var sortOrder = GetToplistSortOrder(sortOrderParam);
            var pageProperties = _pagePropertiesFactory.Create(homegame);
            var tableModel = _cashgameToplistTableModelFactory.Create(homegame, suite, year, sortOrder);
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