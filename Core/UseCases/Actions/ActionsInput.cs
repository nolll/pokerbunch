namespace Core.UseCases.Actions
{
    public class ActionsInput
    {
        public string Slug { get; private set; }
        public string DateStr { get; private set; }
        public int PlayerId { get; private set; }

        public ActionsInput(string slug, string dateStr, int playerId)
        {
            Slug = slug;
            DateStr = dateStr;
            PlayerId = playerId;
        }
    }
}