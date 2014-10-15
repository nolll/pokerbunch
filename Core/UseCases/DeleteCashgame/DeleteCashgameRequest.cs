namespace Core.UseCases.DeleteCashgame
{
    public class DeleteCashgameRequest
    {
        public string Slug { get; private set; }
        public string DateStr { get; private set; }

        public DeleteCashgameRequest(string slug, string dateStr)
        {
            Slug = slug;
            DateStr = dateStr;
        }
    }
}