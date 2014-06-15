namespace Application.Urls
{
    public class Url
    {
        public string Relative { get; private set; }

        public Url(string url)
        {
            Relative = url ?? string.Empty;
        }

        public override string ToString()
        {
            return Relative;
        }

        public virtual bool IsEmpty()
        {
            return false;
        }
    }
}