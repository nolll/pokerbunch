namespace Core.Urls
{
    public class DeletePlayerUrl : PlayerUrl
    {
        public DeletePlayerUrl(string slug, int playerId)
            : base(Routes.PlayerDelete, slug, playerId)
        {
        }
    }
}