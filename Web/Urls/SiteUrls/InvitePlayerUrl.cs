using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class InvitePlayerUrl : SiteUrl
    {
        private readonly string _id;

        public InvitePlayerUrl(string id)
        {
            _id = id;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.Player.Invite, RouteReplace.Id(_id));
    }
}