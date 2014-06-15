namespace Application.Urls
{
    public class AddPlayerUrl : HomegameUrl
    {
        public AddPlayerUrl(string slug)
            : base(RouteFormats.PlayerAdd, slug)
        {
        }
    }
}