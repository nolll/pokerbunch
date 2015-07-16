namespace Core.UseCases.CashgameDetails
{
    public class CashgameDetailsRequest
    {
        public string Slug { get; private set; }
        public string UserName { get; private set; }
        public string DateStr { get; private set; }

        public CashgameDetailsRequest(string slug, string userName, string dateStr)
        {
            Slug = slug;
            UserName = userName;
            DateStr = dateStr;
        }
    }
}