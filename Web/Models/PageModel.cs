using System.Collections.Generic;
using Core.Classes;
using Web.Models;

namespace core{

	public class PageModel {

	    public List<string> ValidationErrors { get; set; }
        //todo:Flytta UserNavigationModel
	    //public UserNavigationModel UserNavModel { get; set; }
	    public GoogleAnalyticsModel GoogleAnalyticsModel { get; set; }

	    public PageModel(User user)
	    {
	        //UserNavModel = new UserNavigationModel(user);
			GoogleAnalyticsModel = new GoogleAnalyticsModel();
	    }

		public void SetValidationErrors(List<string> errors){
			ValidationErrors = errors;
		}

	}

}