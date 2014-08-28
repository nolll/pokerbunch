namespace Application.Urls
{
    public class JoinBunchConfirmationUrl : BunchUrl
    {
        public JoinBunchConfirmationUrl(string slug)
            : base(RouteFormats.BunchJoinConfirmation, slug)
        {
        }
    }
}