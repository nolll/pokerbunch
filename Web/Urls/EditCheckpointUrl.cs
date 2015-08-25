using Core.Urls;

namespace Web.Urls
{
    public class EditCheckpointUrl : IdUrl
    {
        public EditCheckpointUrl(int checkpointId)
            : base(Routes.CashgameCheckpointEdit, checkpointId)
        {
        }
    }
}