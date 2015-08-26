namespace Web.Urls
{
    public class DeletePlayerUrl : IdUrl
    {
        public DeletePlayerUrl(int playerId)
            : base(Routes.PlayerDelete, playerId)
        {
        }
    }
}