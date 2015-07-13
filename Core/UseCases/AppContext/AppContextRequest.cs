namespace Core.UseCases.AppContext
{
    public class AppContextRequest
    {
        public string UserName { get; private set; }

        public AppContextRequest(string userName)
        {
            UserName = userName;
        }
    }
}