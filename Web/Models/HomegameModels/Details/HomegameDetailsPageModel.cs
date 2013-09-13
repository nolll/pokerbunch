using Core.Classes;
using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.Models.HomegameModels.Details{

	public class HomegameDetailsPageModel : PageProperties {

	    public string DisplayName { get; set; }
	    public string Description { get; set; }
	    public string HouseRules { get; set; }
	    public bool ShowHouseRules { get; set; }
	    public UrlModel EditUrl { get; set; }
	    public bool ShowEditLink { get; set; }

	    public HomegameDetailsPageModel(User user, Homegame homegame, bool isInManagerMode, Cashgame runningGame = null) : base(user, homegame, runningGame)
	    {
	        DisplayName = homegame.DisplayName;
			Description = homegame.Description;
			HouseRules = FormatHouseRules(homegame.HouseRules);
	        ShowHouseRules = !string.IsNullOrEmpty(HouseRules);
			EditUrl = new HomegameEditUrlModel(homegame);
			ShowEditLink = isInManagerMode;
	    }

		private string FormatHouseRules(string houseRules)
		{
		    if (houseRules == null)
		        return null;
		    return houseRules.Replace("\n\r", "<br />\n\r");
		}

        public override string BrowserTitle
        {
            get
            {
                return "Homegame Details";
            }
        }

	}

}