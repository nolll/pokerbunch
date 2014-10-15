namespace Core.UseCases.ActionsChart
{
    public class ActionsChartRequest
    {
        public string Slug { get; private set; }
        public string DateStr { get; private set; }
        public int PlayerId { get; private set; }

        public ActionsChartRequest(string slug, string dateStr, int playerId)
        {
            Slug = slug;
            DateStr = dateStr;
            PlayerId = playerId;
        }
    }
}