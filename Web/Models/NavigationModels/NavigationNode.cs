namespace Web.Models.NavigationModels
{
    public class NavigationNode
    {
        public string Name { get; }
        public string Url { get; }
        private bool Selected { get; }
        public string CssClass => Selected ? "selected" : null;

        public NavigationNode(string name, string url, bool selected = false)
        {
            Name = name;
            Url = url;
            Selected = selected;
        }
    }
}