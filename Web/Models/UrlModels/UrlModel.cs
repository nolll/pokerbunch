namespace Web.Models.UrlModels
{
    public class UrlModel
    {
        public string Relative { get; private set; }

        public UrlModel(string url)
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

    public class EmptyUrlModel : UrlModel
    {
        public EmptyUrlModel() : base(string.Empty)
        {
        }

        public override bool IsEmpty()
        {
            return true;
        }
    }
}