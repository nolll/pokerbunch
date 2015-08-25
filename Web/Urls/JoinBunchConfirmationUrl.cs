using Core.Urls;

namespace Web.Urls
{
    public class JoinBunchConfirmationUrl : SlugUrl
    {
        public JoinBunchConfirmationUrl(string slug)
            : base(Routes.BunchJoinConfirmation, slug)
        {
        }
    }
}