namespace Web.Controllers.Base
{
    public abstract class JsonViewModel
    {
        public virtual bool Success
        {
            get { return false; }
        }
    }
}