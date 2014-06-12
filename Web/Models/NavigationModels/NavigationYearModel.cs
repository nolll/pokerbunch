using Web.Models.UrlModels;

namespace Web.Models.NavigationModels
{
    public class NavigationYearModel
    {
        public Url Url { get; private set; }
        public string Text { get; private set; }

        public NavigationYearModel(Url link, string text)
        {
            Url = link;
            Text = text;
        }
    }
}
