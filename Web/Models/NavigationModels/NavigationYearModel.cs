using Web.Urls;

namespace Web.Models.NavigationModels
{
    public class NavigationYearModel
    {
        public string Text { get; private set; }
        public string Url { get; private set; }
        public string SelectedCssClass { get; private set; }

        public NavigationYearModel(string label, SiteUrl url, bool isSelected)
        {
            Text = label;
            Url = url.Relative;
            SelectedCssClass = isSelected ? "selected" : null;
        }
    }
}
