namespace Core.Urls
{
    public class EmptyUrl : Url
    {
        public EmptyUrl() : base(null)
        {
        }

        public override bool IsEmpty()
        {
            return true;
        }
    }
}