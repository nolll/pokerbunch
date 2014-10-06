namespace Core.Urls
{
    public class JoinBunchConfirmationUrl : BunchUrl
    {
        public JoinBunchConfirmationUrl(string slug)
            : base(RouteFormats.BunchJoinConfirmation, slug)
        {
        }
    }
}