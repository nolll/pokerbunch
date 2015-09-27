using Web.Common.Routes;

namespace Web.Urls
{
    public class EditCheckpointUrl : IdUrl
    {
        public EditCheckpointUrl(int checkpointId)
            : base(WebRoutes.CashgameCheckpointEdit, checkpointId)
        {
        }
    }
}