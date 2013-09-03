using System.Collections.Generic;
using System.Globalization;
using Core.Classes;
using Web.Models.UrlModels;

namespace Web.Models.NavigationModels{

    public class CashgameYearNavigationModel{

        public string Selected { get; set; }
        public List<NavigationYearModel> YearModels { get; set; }

		public CashgameYearNavigationModel(Homegame homegame, IList<int> years, int? year = null, string view = null){
			Selected = year.HasValue ? year.Value.ToString(CultureInfo.InvariantCulture) : "All Time";
			YearModels = GetYearModels(homegame, view, years);
		}

		private List<NavigationYearModel> GetYearModels(Homegame homegame, string view, IList<int> years)
		{
		    var yearModels = new List<NavigationYearModel>();
            if(years != null){
				for(var i = 0; i < years.Count; i++){
					var year = years[i];
					yearModels.Add(new NavigationYearModel(GetNavigationUrl(homegame, view, year), year.ToString(CultureInfo.InvariantCulture)));
				}
				yearModels.Add(new NavigationYearModel(GetNavigationUrl(homegame, view), "All Time"));
			}
		    return yearModels;
		}

		private UrlModel GetNavigationUrl(Homegame homegame, string view, int? year = null){
			if(view == "matrix"){
				return new CashgameMatrixUrlModel(homegame, year);
			}
			if(view == "leaderboard"){
				return new CashgameLeaderboardUrlModel(homegame, year);
			}
			if(view == "chart"){
				return new CashgameChartUrlModel(homegame, year);
			}
			if(view == "listing"){
				return new CashgameListingUrlModel(homegame, year);
			}
			if(view == "facts"){
				return new CashgameFactsUrlModel(homegame, year);
			}
			return null;
		}

	}

}
