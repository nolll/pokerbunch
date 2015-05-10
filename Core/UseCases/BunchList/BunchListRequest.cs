namespace Core.UseCases.BunchList
{
    public class BunchListRequest
    {
        public int UserId { get; private set; }

        public BunchListRequest(int userId)
        {
            UserId = userId;
        }
    }
}