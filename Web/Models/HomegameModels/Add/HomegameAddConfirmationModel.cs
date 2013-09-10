using Core.Classes;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Add{

	public class HomegameAddConfirmationModel : PageModel {

	    public HomegameAddConfirmationModel(User user)
	    {
	        
	    }

        public override string BrowserTitle
        {
            get
            {
                return "Homegame Created";
            }
        }

	}

}