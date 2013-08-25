using Core.Classes;
using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.Models.HomeModels{

	public class HomePageModel : HomegamePageModel {

	    public bool IsLoggedIn { get; private set; }
	    public UrlModel AddHomegameUrl { get; private set; }
        public UrlModel LoginUrl { get; private set; }
        public UrlModel RegisterUrl { get; private set; }
	    public AdminNavModel AdminNav { get; private set; }

	    public HomePageModel(User user, Homegame homegame, Cashgame runningGame) : base(user, homegame, runningGame)
	    {
			IsLoggedIn = user != null;
            AddHomegameUrl = new HomegameAddUrlModel();
            LoginUrl = new AuthLoginUrlModel();
            RegisterUrl = new UserAddUrlModel();
			AdminNav = new AdminNavModel(user);
	    }

	}

}