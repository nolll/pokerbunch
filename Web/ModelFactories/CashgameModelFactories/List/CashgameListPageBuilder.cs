using System;
using Application.Services;
using Application.UseCases.CashgameContext;
using Core.Repositories;
using Plumbing;
using Web.Models.CashgameModels.List;

namespace Web.ModelFactories.CashgameModelFactories.List
{
    public class CashgameListPageBuilder : ICashgameListPageBuilder
    {
        private readonly ICashgameListTableModelFactory _cashgameListTableModelFactory;
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IWebContext _webContext;

        public CashgameListPageBuilder(
            ICashgameListTableModelFactory cashgameListTableModelFactory,
            IBunchRepository bunchRepository,
            ICashgameRepository cashgameRepository,
            IWebContext webContext)
        {
            _cashgameListTableModelFactory = cashgameListTableModelFactory;
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _webContext = webContext;
        }

        public CashgameListPageModel Build(string slug, int? year)
        {
            var homegame = _bunchRepository.GetBySlug(slug);
            var cashgames = _cashgameRepository.GetPublished(homegame, year);
            var sortOrder = GetListSortOrder();

            var contextResult = DependencyContainer.Instance.CashgameContext(new CashgameContextRequest(slug, year, CashgamePage.List));

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