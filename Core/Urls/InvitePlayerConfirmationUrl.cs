namespace Core.Urls
{
    public class InvitePlayerConfirmationUrl : PlayerUrl
    {
        public InvitePlayerConfirmationUrl(string slug, int playerId)
            : base(RouteFormats.PlayerInviteConfirmation, slug, playerId)
        {
        }
    }
}