using Web.Common.Routes;

namespace Web.Urls
{
    public class DeleteCheckpointUrl : IdUrl
    {
        public DeleteCheckpointUrl(int id)
            : base(WebRoutes.CashgameCheckpointDelete, id)
        {
        }
    }
}