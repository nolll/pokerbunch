using System.Collections.Generic;
using Core.Classes;
using Web.Models.FormModels;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Add{

	public class AddCashgamePageModel : AddCashgamePostModel, IPageModel {

        public string BrowserTitle { get; set; }
        public PageProperties PageProperties { get; set; }
	    public string Location { get; set; }
	    public LocationFieldModel LocationSelectModel { get; set; }

        public AddCashgamePageModel(User user, Homegame homegame, IEnumerable<string> locations)
        {
            BrowserTitle = "New Cashgame";
            PageProperties = new PageProperties(user, homegame);
            LocationSelectModel = new LocationFieldModel("location", "location", Location, locations, "choose one");
        }

        public AddCashgamePageModel(User user, Homegame homegame, IEnumerable<string> locations, AddCashgamePostModel postModel)
            : this(user, homegame, locations)
    	{
	        Location = postModel.Location;
	    }

	}

}