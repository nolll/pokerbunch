using Core.Classes;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Add{

	public class HomegameAddConfirmationModel : IPageModel {

	    public string BrowserTitle { get; set; }
	    public PageProperties PageProperties { get; set; }

	    public HomegameAddConfirmationModel(User user)
	    {
	        BrowserTitle = "Homegame Created";
            PageProperties = new PageProperties(user);
	    }

	}

}