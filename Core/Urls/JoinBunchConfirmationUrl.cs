namespace Core.Urls
{
    public class JoinBunchConfirmationUrl : SlugUrl
    {
        public JoinBunchConfirmationUrl(string slug)
            : base(Routes.BunchJoinConfirmation, slug)
        {
        }
    }
}