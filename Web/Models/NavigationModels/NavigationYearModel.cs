using Web.Models.UrlModels;

namespace Web.Models.NavigationModels
{
    public class NavigationYearModel
    {
        public UrlModel Url { get; private set; }
        public string Text { get; private set; }

        public NavigationYearModel(UrlModel link, string text)
        {
            Url = link;
            Text = text;
        }
    }
}
