using Web.Common.Routes;

namespace Web.Urls
{
    public class JoinBunchConfirmationUrl : SlugUrl
    {
        public JoinBunchConfirmationUrl(string slug)
            : base(WebRoutes.BunchJoinConfirmation, slug)
        {
        }
    }
}