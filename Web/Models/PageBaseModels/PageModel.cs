using System.Collections.Generic;
using Core.Classes;
using Web.Models.MiscModels;
using Web.Models.NavigationModels;

namespace Web.Models.PageBaseModels{

	public class PageModel {

	    public List<string> ValidationErrors { get; set; }
	    public UserNavigationModel UserNavModel { get; set; }
        public HomegameNavigationModel HomegameNavModel { get; protected set; }
	    public GoogleAnalyticsModel GoogleAnalyticsModel { get; set; }

	    public PageModel()
	    {
            ValidationErrors = new List<string>();
	    }

	    public PageModel(User user)
            : this() 
	    {
	        UserNavModel = new UserNavigationModel(user);
			GoogleAnalyticsModel = new GoogleAnalyticsModel();
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