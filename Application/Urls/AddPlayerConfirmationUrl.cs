namespace Application.Urls
{
    public class AddPlayerConfirmationUrl : BunchUrl
    {
        public AddPlayerConfirmationUrl(string slug)
            : base(RouteFormats.PlayerAddConfirmation, slug)
        {
        }
    }
}