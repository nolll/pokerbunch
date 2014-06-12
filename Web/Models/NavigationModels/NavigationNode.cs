using Web.Models.UrlModels;

namespace Web.Models.NavigationModels
{
    public class NavigationNode
    {
        public string Name { get; private set; }
        public UrlModel Url { get; private set; }
        public bool Selected { get; private set; }

        public NavigationNode(string name, UrlModel urlModel, bool selected = false)
        {
            Name = name;
            Url = urlModel;
            Selected = selected;
        }

        public string CssClass
        {
            get { return Selected ? "selected" : null; }
        }
    }
}