using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Extensions;

namespace Web.Models.NavigationModels
{
    public class CashgameYearNavigationModel : IViewModel
    {
        private const string AllText = "All";

        public IList<NavigationYearModel> YearModels { get; }
        public string SelectedYear { get; }

        public CashgameYearNavigationModel(CashgameContext.Result cashgameContext)
        {
            YearModels = GetYearModels(cashgameContext);
            SelectedYear = cashgameContext.SelectedYear.HasValue ? cashgameContext.SelectedYear.ToString() : AllText;
        }

        private IList<NavigationYearModel> GetYearModels(CashgameContext.Result cashgameContext)
        {
            var models = cashgameContext.Years.Select(o => GetYearModel(cashgameContext.BunchId, cashgameContext.SelectedYear, cashgameContext.SelectedPage, o)).ToList();
            models.Add(GetYearModel(cashgameContext.BunchId, cashgameContext.SelectedYear, cashgameContext.SelectedPage));
            return models;
        } 

        private NavigationYearModel GetYearModel(string slug, int? selectedYear, CashgameContext.CashgamePage selectedPage, int? year = null)
        {
            var label = year.HasValue ? year.Value.ToString(CultureInfo.InvariantCulture) : AllText;
            var url = GetYearUrl(slug, selectedPage, year);
            var isSelected = selectedYear == year;
            return new NavigationYearModel(label, url, isSelected);
        } 

        private SiteUrl GetYearUrl(string slug, CashgameContext.CashgamePage cashgamePage, int? year = null)
        {
            if (cashgamePage.Equals(CashgameContext.CashgamePage.Overview))
                return new CashgameIndexUrl(slug);
            if (cashgamePage.Equals(CashgameContext.CashgamePage.Matrix))
                return year.HasValue ? (SiteUrl)new MatrixWithYearUrl(slug, year) : new MatrixUrl(slug);
            if (cashgamePage.Equals(CashgameContext.CashgamePage.Toplist))
                return year.HasValue ? (SiteUrl)new TopListWithYearUrl(slug, year) : new TopListUrl(slug);
            if (cashgamePage.Equals(CashgameContext.CashgamePage.Chart))
                return year.HasValue ? (SiteUrl)new ChartWithYearUrl(slug, year) : new ChartUrl(slug);
            if (cashgamePage.Equals(CashgameContext.CashgamePage.List))
                return year.HasValue ? (SiteUrl)new ListWithYearUrl(slug, year) : new ListUrl(slug);
            if (cashgamePage.Equals(CashgameContext.CashgamePage.Facts))
                return year.HasValue ? (SiteUrl)new FactsWithYearUrl(slug, year) : new FactsUrl(slug);
            return null;
        }

        public View GetView()
        {
            return new View("~/Views/Navigation/CashgameYearNavigation.cshtml");
        }
    }
}
