using Application.Urls;

namespace Web.Models.NavigationModels
{
    public class NavigationNode
    {
        public string Name { get; private set; }
        public string Url { get; private set; }
        public bool Selected { get; private set; }

        public NavigationNode(string name, Url url, bool selected = false)
        {
            Name = name;
            Url = url.Relative;
            Selected = selected;
        }

        public string CssClass
        {
            get { return Selected ? "selected" : null; }
        }
    }
}