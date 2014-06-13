using Core.Services.Interfaces;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.NavigationModelFactories
{
    public class CashgamePageNavigationModelFactory : ICashgamePageNavigationModelFactory
    {
        private readonly ICashgameService _cashgameService;

        public CashgamePageNavigationModelFactory(
            ICashgameService cashgameService)
        {
            _cashgameService = cashgameService;
        }

        public CashgamePageNavigationModel Create(string slug, CashgamePage cashgamePage)
        {
            var year = _cashgameService.GetLatestYear(slug);
            return new CashgamePageNavigationModel(slug, year, cashgamePage);
        }
    }
}