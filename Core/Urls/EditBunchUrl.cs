namespace Core.Urls
{
    public class EditBunchUrl : BunchUrl
    {
        public EditBunchUrl(string slug)
            : base(RouteFormats.BunchEdit, slug)
        {
        }
    }
}