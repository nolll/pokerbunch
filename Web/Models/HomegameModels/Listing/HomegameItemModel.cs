using Core.Classes;
using Web.Models.Url;

namespace Web.Models.HomegameModels.Listing{

	public class HomegameItemModel{

	    public string Name { get; set; }
	    public UrlModel UrlModel { get; set; }

	    public HomegameItemModel(Homegame homegame)
	    {
	        Name = homegame.DisplayName;
			UrlModel = new HomegameDetailsUrlModel(homegame);
	    }
        
	}

}