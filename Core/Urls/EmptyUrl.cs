namespace Core.Urls
{
    public class EmptyUrl : Url
    {
        public EmptyUrl() : base(null)
        {
        }

        public virtual bool IsEmpty()
        {
            return true;
        }
    }
}