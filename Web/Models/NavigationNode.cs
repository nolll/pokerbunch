namespace Web.Models{

	public class NavigationNode{

	    public string Name { get; private set; }
	    public UrlModel UrlModel { get; private set; }
	    public bool Selected { get; private set; }

	    public NavigationNode(string name, UrlModel urlModel, bool selected)
	    {
	        Name = name;
	        UrlModel = urlModel;
	        Selected = selected;
	    }

	}

}