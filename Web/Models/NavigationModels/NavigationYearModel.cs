using Web.Models.UrlModels;

namespace Web.Models.NavigationModels{
    public class NavigationYearModel{

	    public UrlModel Link { get; set; }
	    public string Text { get; set; }

        public NavigationYearModel(UrlModel link, string text){
			Link = link;
			Text = text;
		}

	}

}
