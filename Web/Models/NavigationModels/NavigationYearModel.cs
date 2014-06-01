using Web.Services;

namespace Web.Models.NavigationModels
{
    public class NavigationYearModel
    {
        public UrlModel Link { get; private set; }
        public string Text { get; private set; }

        public NavigationYearModel(UrlModel link, string text)
        {
            Link = link;
            Text = text;
        }
    }
}
