using Web.Models.UrlModels;

namespace Web.Models.NavigationModels{

	public class HomegameNavigationModel{

	    public string Heading { get; set; }
	    public UrlModel HeadingLink { get; set; }
        public UrlModel CashgameLink { get; set; }
        public UrlModel PlayerLink { get; set; }
        public UrlModel CreateLink { get; set; }
        public UrlModel RunningLink { get; set; }
		public bool CashgameIsRunning { get; set; }

	}

}
