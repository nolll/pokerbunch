namespace Web.Urls
{
    public class AddPlayerUrl : SlugUrl
    {
        public AddPlayerUrl(string slug)
            : base(Routes.PlayerAdd, slug)
        {
        }
    }
}