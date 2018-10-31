namespace Web.Controllers.Base
{
    public class JsonTokenViewModel : JsonViewModel
    {
        public string Token { get; }
        public override bool Success => true;

        public JsonTokenViewModel(string token)
        {
            Token = token;
        }
    }
}