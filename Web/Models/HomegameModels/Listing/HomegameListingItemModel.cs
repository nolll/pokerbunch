using Core.Classes;
using Web.Models.UrlModels;

namespace Web.Models.HomegameModels.Listing{

	public class HomegameListingItemModel{

	    public string Name { get; set; }
	    public UrlModel UrlModel { get; set; }

	    public HomegameListingItemModel(Homegame homegame)
	    {
	        Name = homegame.DisplayName;
			UrlModel = new HomegameDetailsUrlModel(homegame);
	    }
        
	}

}