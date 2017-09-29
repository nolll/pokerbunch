using Web.Extensions;

namespace Web.Components.ApiDocsModels.NavigationBlock
{
    public class NavigationItemModel : Component
    {
        public string Name { get; }
        public string Url { get; }

        public NavigationItemModel(string name, string url)
        {
            Name = name;
            Url = url;
        }
    }
}