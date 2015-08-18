namespace Core.Urls
{
    public class AddPlayerConfirmationUrl : SlugUrl
    {
        public AddPlayerConfirmationUrl(string slug)
            : base(Routes.PlayerAddConfirmation, slug)
        {
        }
    }
}