using Web.Models.UrlModels;

namespace Web.Models.NavigationModels
{
    public class NavigationNode
    {
        public string Name { get; private set; }
        public Url Url { get; private set; }
        public bool Selected { get; private set; }

        public NavigationNode(string name, Url url, bool selected = false)
        {
            Name = name;
            Url = url;
            Selected = selected;
        }

        public string CssClass
        {
            get { return Selected ? "selected" : null; }
        }
    }
}