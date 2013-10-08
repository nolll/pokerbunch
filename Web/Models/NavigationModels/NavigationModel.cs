using System.Collections.Generic;

namespace Web.Models.NavigationModels{

	public class NavigationModel {

	    public string Heading { get; set; }
	    public bool HeadingIsLinked { get; set; }
	    public IList<NavigationNode> Nodes { get; set; }
	    public string CssClass { get; set; }
	    public string DataRequire { get; set; }

	    public NavigationModel()
	    {
	        Nodes = new List<NavigationNode>();
	    }

	}

}