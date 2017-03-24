using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class InvitePlayerConfirmationUrl : SiteUrl
    {
        private readonly string _id;

        public InvitePlayerConfirmationUrl(string id)
        {
            _id = id;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.Player.InviteConfirmation, RouteParam.Id(_id));
    }
}