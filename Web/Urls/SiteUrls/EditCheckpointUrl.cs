using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class EditCheckpointUrl : SiteUrl
    {
        private readonly string _cashgameId;
        private readonly string _id;

        public EditCheckpointUrl(string cashgameId, string id)
        {
            _cashgameId = cashgameId;
            _id = id;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.Cashgame.CheckpointEdit, RouteReplace.CashgameId(_cashgameId), RouteReplace.Id(_id));
    }
}