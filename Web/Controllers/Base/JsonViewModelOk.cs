namespace Web.Controllers.Base
{
    public class JsonViewModelOk : JsonViewModel
    {
        public override bool Success
        {
            get { return true; }
        }
    }
}