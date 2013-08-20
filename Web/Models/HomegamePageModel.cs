using Core.Classes;
using Web.Models.Navigation;

namespace Web.Models{

	public class HomegamePageModel : PageModel{

	    public HomegamePageModel(User user, Homegame homegame, Cashgame runningGame) : base(user)
	    {
	        if(homegame != null){
			    HomegameNavModel = new HomegameNavigationModel(homegame, runningGame);
			}
	    }

	}

}