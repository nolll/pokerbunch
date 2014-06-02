using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Application.UseCases.CashgameContext;
using Web.Models.UrlModels;
using Web.Services;

namespace Web.Models.NavigationModels
{
    public class CashgameYearNavigationModel
    {
        public string Selected { get; set; }
        public List<NavigationYearModel> YearModels { get; set; }

        public CashgameYearNavigationModel(string slug, IList<int> years, CashgamePage cashgamePage, int? year)
        {
            Selected = year.HasValue ? year.Value.ToString(CultureInfo.InvariantCulture) : "All Time";
            YearModels = GetYearModels(slug, cashgamePage, years);
        }

        public CashgameYearNavigationModel(CashgameContextResult cashgameContextResult, CashgamePage cashgamePage)
            : this(cashgameContextResult.Slug, cashgameContextResult.Years, cashgamePage, cashgameContextResult.SelectedYear)
        {
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

        private UrlModel GetNavigationUrl(string slug, CashgamePage cashgamePage, int? year = null)
        {
            if (cashgamePage.Equals(CashgamePage.Matrix))
            {
                return new MatrixUrlModel(slug, year);
            }
            if (cashgamePage.Equals(CashgamePage.Toplist))
            {
                return new TopListUrlModel(slug, year);
            }
            if (cashgamePage.Equals(CashgamePage.Chart))
            {
                return new ChartUrlModel(slug, year);
            }
            if (cashgamePage.Equals(CashgamePage.List))
            {
                return new ListUrlModel(slug, year);
            }
            if (cashgamePage.Equals(CashgamePage.Facts))
            {
                return new FactsUrlModel(slug, year);
            }
            return null;
        }
    }
}
