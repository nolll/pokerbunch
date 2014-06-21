using System;
using Application.Services;
using Application.UseCases.BunchContext;
using Core.Repositories;
using Web.ModelFactories.NavigationModelFactories;
using Web.Models.CashgameModels.List;
using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;

namespace Web.ModelFactories.CashgameModelFactories.List
{
    public class CashgameListPageBuilder : ICashgameListPageBuilder
    {
        private readonly ICashgameListTableModelFactory _cashgameListTableModelFactory;
        private readonly ICashgamePageNavigationModelFactory _cashgamePageNavigationModelFactory;
        private readonly ICashgameYearNavigationModelFactory _cashgameYearNavigationModelFactory;
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IWebContext _webContext;
        private readonly IBunchContextInteractor _contextInteractor;

        public CashgameListPageBuilder(
            ICashgameListTableModelFactory cashgameListTableModelFactory,
            ICashgamePageNavigationModelFactory cashgamePageNavigationModelFactory,
            ICashgameYearNavigationModelFactory cashgameYearNavigationModelFactory,
            IHomegameRepository homegameRepository,
            ICashgameRepository cashgameRepository,
            IWebContext webContext,
            IBunchContextInteractor contextInteractor)
        {
            _cashgameListTableModelFactory = cashgameListTableModelFactory;
            _cashgamePageNavigationModelFactory = cashgamePageNavigationModelFactory;
            _cashgameYearNavigationModelFactory = cashgameYearNavigationModelFactory;
            _homegameRepository = homegameRepository;
            _cashgameRepository = cashgameRepository;
            _webContext = webContext;
            _contextInteractor = contextInteractor;
        }

        public CashgameListPageModel Build(string slug, int? year)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var cashgames = _cashgameRepository.GetPublished(homegame, year);
            var years = _cashgameRepository.GetYears(homegame);
            var sortOrder = GetListSortOrder();

            var contextResult = _contextInteractor.Execute(new BunchContextRequest(slug));

            return new CashgameListPageModel
                {
                    BrowserTitle = "Cashgame List",
                    PageProperties = new PageProperties(contextResult),
			        ListTableModel = _cashgameListTableModelFactory.Create(homegame, cashgames, sortOrder, year),
                    PageNavModel = _cashgamePageNavigationModelFactory.Create(homegame.Slug, CashgamePage.List),
                    YearNavModel = _cashgameYearNavigationModelFactory.Create(homegame.Slug, years, CashgamePage.List, year)
                };
        }

        private ListSortOrder GetListSortOrder()
        {
            var param = _webContext.GetQueryParam("orderby");
            if (param == null)
            {
                return ListSortOrder.date;
            }
            ListSortOrder sortOrder;
            if (Enum.TryParse(param, out sortOrder))
            {
                return sortOrder;
            }
            return ListSortOrder.date;
        }
    }
}