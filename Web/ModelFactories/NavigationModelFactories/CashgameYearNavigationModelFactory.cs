using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Application.Services;
using Application.UseCases.CashgameContext;
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

        public CashgameYearNavigationModel Create(string slug, IList<int> years, CashgamePage cashgamePage, int? year)
        {
            return new CashgameYearNavigationModel
                {
                    Selected = year.HasValue ? year.Value.ToString(CultureInfo.InvariantCulture) : "All Time",
                    YearModels = GetYearModels(slug, cashgamePage, years),
                };
        }

        public CashgameYearNavigationModel Create(CashgameContextResult cashgameContextResult, CashgamePage cashgamePage)
        {
            return Create(cashgameContextResult.Slug, cashgameContextResult.Years, cashgamePage, cashgameContextResult.SelectedYear);
        }

        private List<NavigationYearModel> GetYearModels(string slug, CashgamePage cashgamePage, IList<int> years)
        {
            var yearModels = new List<NavigationYearModel>();
            if (years != null)
            {
                yearModels.AddRange(years.Select(year => new NavigationYearModel(GetNavigationUrl(slug, cashgamePage, year), year.ToString(CultureInfo.InvariantCulture))));
                yearModels.Add(new NavigationYearModel(GetNavigationUrl(slug, cashgamePage), "All Time"));
            }
            return yearModels;
        }

        private string GetNavigationUrl(string slug, CashgamePage cashgamePage, int? year = null)
        {
            if (cashgamePage.Equals(CashgamePage.Matrix))
            {
                return _urlProvider.GetCashgameMatrixUrl(slug, year);
            }
            if (cashgamePage.Equals(CashgamePage.Toplist))
            {
                return _urlProvider.GetCashgameToplistUrl(slug, year);
            }
            if (cashgamePage.Equals(CashgamePage.Chart))
            {
                return _urlProvider.GetCashgameChartUrl(slug, year);
            }
            if (cashgamePage.Equals(CashgamePage.List))
            {
                return _urlProvider.GetCashgameListUrl(slug, year);
            }
            if (cashgamePage.Equals(CashgamePage.Facts))
            {
                return _urlProvider.GetCashgameFactsUrl(slug, year);
            }
            return null;
        }
    }
}