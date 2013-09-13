using System.Collections.Generic;
using Core.Classes;
using Web.Models.MiscModels;
using Web.Models.NavigationModels;

namespace Web.Models.PageBaseModels
{
    public class HomegamePageProperties {

	    public List<string> ValidationErrors { get; set; }
	    public UserNavigationModel UserNavModel { get; set; }
        public HomegameNavigationModel HomegameNavModel { get; protected set; }
	    public GoogleAnalyticsModel GoogleAnalyticsModel { get; set; }

	    public HomegamePageProperties(User user, Homegame homegame = null, Cashgame runningGame = null)
	    {
	        UserNavModel = new UserNavigationModel(user);
			GoogleAnalyticsModel = new GoogleAnalyticsModel();
            if (homegame != null)
            {
                HomegameNavModel = new HomegameNavigationModel(homegame, runningGame);
            }
	    }

		public void SetValidationErrors(List<string> errors){
			ValidationErrors = errors;
		}

	    public virtual string BrowserTitle
	    {
            get { return "Poker Bunch"; }
	    }

	}

}