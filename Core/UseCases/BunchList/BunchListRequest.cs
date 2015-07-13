namespace Core.UseCases.BunchList
{
    public class BunchListRequest
    {
        public string UserName { get; private set; }

        public BunchListRequest(string userName)
        {
            UserName = userName;
        }
    }
}