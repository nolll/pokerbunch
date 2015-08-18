namespace Core.Urls
{
    public class InvitePlayerConfirmationUrl : IdUrl
    {
        public InvitePlayerConfirmationUrl(int playerId)
            : base(Routes.PlayerInviteConfirmation, playerId)
        {
        }
    }
}