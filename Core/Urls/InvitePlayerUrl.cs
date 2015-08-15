namespace Core.Urls
{
    public class InvitePlayerUrl : PlayerUrl
    {
        public InvitePlayerUrl(string slug, int playerId)
            : base(Routes.PlayerInvite, slug, playerId)
        {
        }
    }
}