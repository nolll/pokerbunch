namespace Application.Urls
{
    public class AddPlayerUrl : BunchUrl
    {
        public AddPlayerUrl(string slug)
            : base(RouteFormats.PlayerAdd, slug)
        {
        }
    }
}