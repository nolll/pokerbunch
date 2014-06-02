using System.Collections.Generic;

namespace Web.Models.NavigationModels
{
	public abstract class NavigationModel
    {
	    public string Heading { get; protected set; }
	    public IList<NavigationNode> Nodes { get; protected set; }
	    public string CssClass { get; protected set; }

	    protected NavigationModel()
	    {
            Nodes = new List<NavigationNode>();
	    }
	}
}