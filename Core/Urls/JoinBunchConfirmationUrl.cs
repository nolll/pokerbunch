namespace Core.Urls
{
    public class JoinBunchConfirmationUrl : BunchUrl
    {
        public JoinBunchConfirmationUrl(string slug)
            : base(Routes.BunchJoinConfirmation, slug)
        {
        }
    }
}