namespace Application.Urls
{
    public class EditHomegameUrl : HomegameUrl
    {
        public EditHomegameUrl(string slug)
            : base(RouteFormats.HomegameEdit, slug)
        {
        }
    }
}