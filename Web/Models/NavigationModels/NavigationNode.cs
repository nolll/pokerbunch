using Web.Models.UrlModels;
using Web.Services;

namespace Web.Models.NavigationModels
{
    public class NavigationNode
    {
        public string Name { get; private set; }
        public UrlModel UrlModel { get; private set; }
        public bool Selected { get; private set; }

        public NavigationNode(string name, UrlModel urlModel, bool selected = false)
        {
            Name = name;
            UrlModel = urlModel;
            Selected = selected;
        }

        public string CssClass
        {
            get { return Selected ? "selected" : null; }
        }
    }
}