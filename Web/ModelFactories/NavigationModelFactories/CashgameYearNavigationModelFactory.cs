using System.Collections.Generic;
using System.Globalization;
using Core.Classes;
using Core.Services;
using Web.Models.NavigationModels;
using Web.Models.UrlModels;

namespace Web.ModelFactories.NavigationModelFactories
{
    public class CashgameYearNavigationModelFactory : ICashgameYearNavigationModelFactory
    {
        private readonly IUrlProvider _urlProvider;

        public CashgameYearNavigationModelFactory(IUrlProvider urlProvider)
        {
            _urlProvider = urlProvider;
        }

        public CashgameYearNavigationModel Create(Homegame homegame, IList<int> years, int? year = null, string view = null)
        {
            return new CashgameYearNavigationModel
                {
                    Selected = year.HasValue ? year.Value.ToString(CultureInfo.InvariantCulture) : "All Time",
                    YearModels = GetYearModels(homegame, view, years),
                };
        }

        private List<NavigationYearModel> GetYearModels(Homegame homegame, string view, IList<int> years)
        {
            var yearModels = new List<NavigationYearModel>();
            if (years != null)
            {
                for (var i = 0; i < years.Count; i++)
                {
                    var year = years[i];
                    yearModels.Add(new NavigationYearModel(GetNavigationUrl(homegame, view, year), year.ToString(CultureInfo.InvariantCulture)));
                }
                yearModels.Add(new NavigationYearModel(GetNavigationUrl(homegame, view), "All Time"));
            }
            return yearModels;
        }

        private string GetNavigationUrl(Homegame homegame, string view, int? year = null)
        {
            if (view == "matrix")
            {
                return new CashgameMatrixUrlModel(homegame, year).Url;
            }
            if (view == "leaderboard")
            {
                return new CashgameLeaderboardUrlModel(homegame, year).Url;
            }
            if (view == "chart")
            {
                return _urlProvider.GetCashgameChartUrl(homegame, year);
            }
            if (view == "listing")
            {
                return new CashgameListingUrlModel(homegame, year).Url;
            }
            if (view == "facts")
            {
                return new CashgameFactsUrlModel(homegame, year).Url;
            }
            return null;
        }
    }
}