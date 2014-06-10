namespace Web.Models.UrlModels
{
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