using Core.Classes;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Add{

	public class HomegameAddConfirmationModel : PageProperties {

	    public HomegameAddConfirmationModel(User user)
            : base(user)
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