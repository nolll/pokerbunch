using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using App.Services.Interfaces;
using Core.Classes;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.NavigationModelFactories
{
    public class CashgameYearNavigationModelFactory : ICashgameYearNavigationModelFactory
    {
        private readonly IUrlProvider _urlProvider;

        public CashgameYearNavigationModelFactory(IUrlProvider urlProvider)
        {
            _urlProvider = urlProvider;
        }

        public CashgameYearNavigationModel Create(Homegame homegame, IList<int> years, CashgamePage cashgamePage, int? year)
        {
            return new CashgameYearNavigationModel
                {
                    Selected = year.HasValue ? year.Value.ToString(CultureInfo.InvariantCulture) : "All Time",
                    YearModels = GetYearModels(homegame, cashgamePage, years),
                };
        }

        private List<NavigationYearModel> GetYearModels(Homegame homegame, CashgamePage cashgamePage, IList<int> years)
        {
            var yearModels = new List<NavigationYearModel>();
            if (years != null)
            {
                yearModels.AddRange(years.Select(year => new NavigationYearModel(GetNavigationUrl(homegame, cashgamePage, year), year.ToString(CultureInfo.InvariantCulture))));
                yearModels.Add(new NavigationYearModel(GetNavigationUrl(homegame, cashgamePage), "All Time"));
            }
            return yearModels;
        }

        private string GetNavigationUrl(Homegame homegame, CashgamePage cashgamePage, int? year = null)
        {
            if (cashgamePage.Equals(CashgamePage.Matrix))
            {
                return _urlProvider.GetCashgameMatrixUrl(homegame.Slug, year);
            }
            if (cashgamePage.Equals(CashgamePage.Toplist))
            {
                return _urlProvider.GetCashgameToplistUrl(homegame.Slug, year);
            }
            if (cashgamePage.Equals(CashgamePage.Chart))
            {
                return _urlProvider.GetCashgameChartUrl(homegame.Slug, year);
            }
            if (cashgamePage.Equals(CashgamePage.List))
            {
                return _urlProvider.GetCashgameListUrl(homegame.Slug, year);
            }
            if (cashgamePage.Equals(CashgamePage.Facts))
            {
                return _urlProvider.GetCashgameFactsUrl(homegame.Slug, year);
            }
            return null;
        }
    }
}