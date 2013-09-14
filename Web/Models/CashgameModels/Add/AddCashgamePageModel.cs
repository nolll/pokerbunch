using System.Collections.Generic;
using System.Web.Mvc;
using Core.Classes;
using Web.Models.PageBaseModels;
using System.Linq;

namespace Web.Models.CashgameModels.Add{

	public class AddCashgamePageModel : AddCashgamePostModel, IPageModel {

        public string BrowserTitle { get; set; }
        public PageProperties PageProperties { get; set; }
        public IEnumerable<SelectListItem> Locations { get; set; }

        public AddCashgamePageModel(User user, Homegame homegame, IEnumerable<string> locations)
        {
            BrowserTitle = "New Cashgame";
            PageProperties = new PageProperties(user, homegame);
            Locations = locations.Select(l => new SelectListItem{Text = l, Value = l});
        }

        public AddCashgamePageModel(User user, Homegame homegame, IEnumerable<string> locations, AddCashgamePostModel postModel)
            : this(user, homegame, locations)
    	{
	        TypedLocation = postModel.TypedLocation;
            SelectedLocation = postModel.SelectedLocation;
    	}

	}

}