using System.Collections.Generic;

namespace Web.Models.NavigationModels{

	public class NavigationModel {

	    public string Heading { get; set; }
	    public string HeadingLink { get; set; }
	    public bool HeadingIsLinked { get; set; }
	    public List<NavigationNode> Nodes { get; set; }
	    public string CssClass { get; set; }
	    public string DataRequire { get; set; }

	    public NavigationModel(string heading, List<NavigationNode> nodes, string cssClass)
	    {
	        Nodes = new List<NavigationNode>();
			if(nodes != null){
				Nodes = nodes;
			}
            Heading = heading;
			CssClass = cssClass;
	    }

		protected void AddNode(NavigationNode node){
			Nodes.Add(node);
		}

	}

}