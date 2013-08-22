using System.Collections.Generic;
using System.Linq;
using Core.Classes;
using Web.Models.HomegameModels.Listing;

namespace Web.Models{

	public class HomegameListingModel : PageModel {

	    public IList<HomegameItemModel> HomegameModels { get; set; }

	    public HomegameListingModel(User user, IEnumerable<Homegame> homegames) : base(user)
	    {
	        HomegameModels = GetHomegameModels(homegames);
	    }

		private IList<HomegameItemModel> GetHomegameModels(IEnumerable<Homegame> homegames)
		{
		    return homegames.Select(homegame => new HomegameItemModel(homegame)).ToList();
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