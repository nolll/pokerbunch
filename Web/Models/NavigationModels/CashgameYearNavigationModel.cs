using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Core.UseCases;
using Web.Extensions;
using Web.Urls.SiteUrls;

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
                return new MatrixUrl(slug, year);
            if (cashgamePage.Equals(CashgameContext.CashgamePage.Toplist))
                return new TopListUrl(slug, year);
            if (cashgamePage.Equals(CashgameContext.CashgamePage.Chart))
                return new ChartUrl(slug, year);
            if (cashgamePage.Equals(CashgameContext.CashgamePage.List))
                return new ListUrl(slug, year);
            if (cashgamePage.Equals(CashgameContext.CashgamePage.Facts))
                return new FactsUrl(slug, year);
            return null;
        }

        public View GetView()
        {
            return new View("~/Views/Navigation/CashgameYearNavigation.cshtml");
        }
    }
}
