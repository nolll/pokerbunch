using Web.Models.UrlModels;

namespace Web.Models.NavigationModels{
    public class NavigationYearModel{

	    public string Link { get; set; }
	    public string Text { get; set; }

        public NavigationYearModel(string link, string text){
			Link = link;
			Text = text;
		}

	}

}
