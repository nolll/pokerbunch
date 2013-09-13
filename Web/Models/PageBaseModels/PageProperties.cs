using Core.Classes;
using Web.Models.MiscModels;
using Web.Models.NavigationModels;

namespace Web.Models.PageBaseModels
{
    public class PageProperties {

	    public UserNavigationModel UserNavModel { get; set; }
        public HomegameNavigationModel HomegameNavModel { get; protected set; }
	    public GoogleAnalyticsModel GoogleAnalyticsModel { get; set; }

	    public PageProperties(User user, Homegame homegame = null, Cashgame runningGame = null)
	    {
	        UserNavModel = new UserNavigationModel(user);
			GoogleAnalyticsModel = new GoogleAnalyticsModel();
            if (homegame != null)
            {
                HomegameNavModel = new HomegameNavigationModel(homegame, runningGame);
            }
	    }

	}

}