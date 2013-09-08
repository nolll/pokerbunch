using System.Collections.Generic;
using Core.Classes;
using Web.Models.FormModels;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Add{

	public class AddCashgamePageModel : HomegamePageModel {

	    public string Date { get; set; }
	    public string Location { get; set; }
	    public LocationFieldModel LocationSelectModel { get; set; }

	    public AddCashgamePageModel(){}

		public AddCashgamePageModel(User user, Homegame homegame, Cashgame cashgame, List<string> locations, List<int> years = null, Cashgame runningGame = null)
            : base(user, homegame, runningGame)
        {
			if(cashgame != null){
				Location = cashgame.Location;
			}
            LocationSelectModel = new LocationFieldModel("location", "location", Location, locations, "choose one");
		}

        public override string BrowserTitle
        {
            get
            {
                return "New Cashgame";
            }
        }
        
	}

}