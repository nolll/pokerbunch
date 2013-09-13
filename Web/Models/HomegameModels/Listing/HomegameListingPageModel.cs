using System.Collections.Generic;
using System.Linq;
using Core.Classes;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Listing{

	public class HomegameListingPageModel : PageProperties {

	    public IList<HomegameListingItemModel> HomegameModels { get; set; }

	    public HomegameListingPageModel(User user, IEnumerable<Homegame> homegames) : base(user)
	    {
	        HomegameModels = GetHomegameModels(homegames);
	    }

		private IList<HomegameListingItemModel> GetHomegameModels(IEnumerable<Homegame> homegames)
		{
		    return homegames.Select(homegame => new HomegameListingItemModel(homegame)).ToList();
		}

        public override string BrowserTitle
        {
            get
            {
                return "Homegame List";
            }
        }
	}

}