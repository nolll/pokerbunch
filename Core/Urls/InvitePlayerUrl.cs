namespace Core.Urls
{
    public class InvitePlayerUrl : IdUrl
    {
        public InvitePlayerUrl(int playerId)
            : base(Routes.PlayerInvite, playerId)
        {
        }
    }
}