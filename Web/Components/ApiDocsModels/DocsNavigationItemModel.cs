using Web.Extensions;

namespace Web.Components.ApiDocsModels
{
    public class DocsNavigationItemModel : Component
    {
        public string Name { get; }
        public string Url { get; }

        public DocsNavigationItemModel(string name, string url)
        {
            Name = name;
            Url = url;
        }
    }
}