using System.Collections.Generic;
using Core.Classes;

namespace Web.Models.NavigationModels{
    public class CashgameNavigationModel{

        public CashgamePageNavigationModel PageNavModel { get; set; }
        public CashgameYearNavigationModel YearNavModel { get; set; }

        public CashgameNavigationModel(Homegame homegame, string view, List<int> years, int? year, Cashgame runningGame){
			PageNavModel = new CashgamePageNavigationModel(homegame, year, view, runningGame);
			YearNavModel = new CashgameYearNavigationModel(homegame, years, year, view);
		}
    
    }

}
