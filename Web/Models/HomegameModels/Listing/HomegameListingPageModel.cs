using System.Collections.Generic;
using System.Linq;
using Core.Classes;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Listing{

	public class HomegameListingPageModel : IPageModel {

	    public string BrowserTitle { get; set; }
	    public PageProperties PageProperties { get; set; }
	    public IList<HomegameListingItemModel> HomegameModels { get; set; }

	    public HomegameListingPageModel(User user, IEnumerable<Homegame> homegames)
	    {
	        BrowserTitle = "Homegame List";
            PageProperties = new PageProperties(user);
	        HomegameModels = GetHomegameModels(homegames);
	    }

		private IList<HomegameListingItemModel> GetHomegameModels(IEnumerable<Homegame> homegames)
		{
		    return homegames.Select(homegame => new HomegameListingItemModel(homegame)).ToList();
		}

	}

}