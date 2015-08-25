using Core.Urls;

namespace Web.Urls
{
    public class DeleteCheckpointUrl : IdUrl
    {
        public DeleteCheckpointUrl(int id)
            : base(Routes.CashgameCheckpointDelete, id)
        {
        }
    }
}