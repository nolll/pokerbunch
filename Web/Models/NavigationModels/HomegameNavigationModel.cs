using Core.Classes;
using Web.Models.UrlModels;

namespace Web.Models.NavigationModels{

	public class HomegameNavigationModel{

	    public string Heading { get; private set; }
	    public UrlModel HeadingLink { get; private set; }
        public UrlModel CashgameLink { get; private set; }
        public UrlModel PlayerLink { get; private set; }
        public UrlModel CreateLink { get; private set; }
        public UrlModel RunningLink { get; private set; }
		public bool CashgameIsRunning { get; private set; }

	    public HomegameNavigationModel(Homegame homegame, Cashgame runningGame)
	    {
	        Heading = homegame.DisplayName;
			HeadingLink = new HomegameDetailsUrlModel(homegame);
			CashgameLink = new CashgameIndexUrlModel(homegame);
			PlayerLink = new PlayerIndexUrlModel(homegame);
			CreateLink = new CashgameAddUrlModel(homegame);
			RunningLink = new RunningCashgameUrlModel(homegame);
			CashgameIsRunning = runningGame != null;
	    }

	}

}
