namespace Web.Models.UrlModels
{
    public abstract class HomegameUrlModel : UrlModel
    {
        protected HomegameUrlModel(string format, string slug)
            : base(string.Format(format, slug))
        {
        }
    }
}