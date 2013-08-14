using System.Collections.Generic;
using Core.Classes;
using Web.Models.Navigation;

namespace Web.Models{

	public class PageModel {

	    public List<string> ValidationErrors { get; set; }
	    public UserNavigationModel UserNavModel { get; set; }
	    public GoogleAnalyticsModel GoogleAnalyticsModel { get; set; }

	    public PageModel(User user)
	    {
	        UserNavModel = new UserNavigationModel(user);
			GoogleAnalyticsModel = new GoogleAnalyticsModel();
	    }

		public void SetValidationErrors(List<string> errors){
			ValidationErrors = errors;
		}

	}

}