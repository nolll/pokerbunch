namespace Core.UseCases.Actions
{
    public class ActionsInput
    {
        public string CurrentUserName { get; private set; }
        public string Slug { get; private set; }
        public string DateStr { get; private set; }
        public int PlayerId { get; private set; }

        public ActionsInput(string currentUserName, string slug, string dateStr, int playerId)
        {
            CurrentUserName = currentUserName;
            Slug = slug;
            DateStr = dateStr;
            PlayerId = playerId;
        }
    }
}