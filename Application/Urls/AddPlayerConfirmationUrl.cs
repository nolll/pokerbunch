namespace Application.Urls
{
    public class AddPlayerConfirmationUrl : HomegameUrl
    {
        public AddPlayerConfirmationUrl(string slug)
            : base(RouteFormats.PlayerAddConfirmation, slug)
        {
        }
    }
}