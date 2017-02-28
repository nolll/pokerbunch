namespace Web.Models.AdminModels
{
    public class ClearCacheModel
    {
        public string Message { get; private set; }

        public ClearCacheModel(string message)
        {
            Message = message;
        }
    }
}