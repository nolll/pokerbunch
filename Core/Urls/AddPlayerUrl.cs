namespace Core.Urls
{
    public class AddPlayerUrl : BunchUrl
    {
        public AddPlayerUrl(string slug)
            : base(Routes.PlayerAdd, slug)
        {
        }
    }
}