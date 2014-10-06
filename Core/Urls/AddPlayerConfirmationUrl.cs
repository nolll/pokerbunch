namespace Core.Urls
{
    public class AddPlayerConfirmationUrl : BunchUrl
    {
        public AddPlayerConfirmationUrl(string slug)
            : base(RouteFormats.PlayerAddConfirmation, slug)
        {
        }
    }
}