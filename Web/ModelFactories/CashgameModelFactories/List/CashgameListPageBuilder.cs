using System;
using Application.Services;
using Application.UseCases.CashgameContext;
using Core.Repositories;
using Web.Models.CashgameModels.List;

namespace Web.ModelFactories.CashgameModelFactories.List
{
    public class CashgameListPageBuilder : ICashgameListPageBuilder
    {
        private readonly ICashgameListTableModelFactory _cashgameListTableModelFactory;
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IWebContext _webContext;
        private readonly ICashgameContextInteractor _contextInteractor;

        public CashgameListPageBuilder(
            ICashgameListTableModelFactory cashgameListTableModelFactory,
            IBunchRepository bunchRepository,
            ICashgameRepository cashgameRepository,
            IWebContext webContext,
            ICashgameContextInteractor contextInteractor)
        {
            _cashgameListTableModelFactory = cashgameListTableModelFactory;
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _webContext = webContext;
            _contextInteractor = contextInteractor;
        }

        public CashgameListPageModel Build(string slug, int? year)
        {
            var homegame = _bunchRepository.GetBySlug(slug);
            var cashgames = _cashgameRepository.GetPublished(homegame, year);
            var sortOrder = GetListSortOrder();

            var contextResult = _contextInteractor.Execute(new CashgameContextRequest(slug, year, CashgamePage.List));

            return new CashgameListPageModel(contextResult)
                {
			        ListTableModel = _cashgameListTableModelFactory.Create(homegame, cashgames, sortOrder, year)
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