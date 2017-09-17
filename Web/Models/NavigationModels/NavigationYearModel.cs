using Web.Urls.SiteUrls;

namespace Web.Models.NavigationModels
{
    public class NavigationYearModel
    {
        public string Text { get; }
        public string Url { get; }
        public string SelectedCssClass { get; }

        public NavigationYearModel(string label, SiteUrl url, bool isSelected)
        {
            Text = label;
            Url = url.Relative;
            SelectedCssClass = isSelected ? "selected" : null;
        }
    }
}
