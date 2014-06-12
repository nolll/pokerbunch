namespace Web.Models.UrlModels
{
    public class EmptyUrl : Url
    {
        public EmptyUrl() : base(string.Empty)
        {
        }

        public override bool IsEmpty()
        {
            return true;
        }
    }
}