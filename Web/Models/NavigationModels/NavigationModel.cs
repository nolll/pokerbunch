using System.Collections.Generic;
using Web.Extensions;

namespace Web.Models.NavigationModels
{
	public abstract class NavigationModel : Component
    {
	    public string Heading { get; protected set; }
	    public IList<NavigationNode> Nodes { get; protected set; }
	    public string CssClass { get; protected set; }
        public bool IsEmpty { get; protected set; }

	    protected NavigationModel()
	    {
            Nodes = new List<NavigationNode>();
	    }

	    public override string ViewName
	    {
	        get { return "~/Views/Navigation/Navigation.cshtml"; }
	    }
    }
}