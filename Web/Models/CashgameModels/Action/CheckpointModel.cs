using Core.Services;
using Core.UseCases;
using Web.Extensions;
using Web.Urls.SiteUrls;

namespace Web.Models.CashgameModels.Action
{
    public class CheckpointModel : IViewModel
    {
        public string Description { get; private set; }
        public string Stack { get; private set; }
        public string Timestamp { get; private set; }
        public bool ShowLink { get; private set; }
        public string EditUrl { get; private set; }

        public CheckpointModel(Actions.CheckpointItem checkpointItem)
        {
            Description = checkpointItem.Type;
            Stack = checkpointItem.DisplayAmount.ToString();
            Timestamp = Globalization.FormatTime(checkpointItem.Time);
            ShowLink = checkpointItem.CanEdit;
            EditUrl = new EditCheckpointUrl(checkpointItem.CashgameId, checkpointItem.CheckpointId).Relative;
        }

        public View GetView()
        {
            return new View("CashgameAction/CheckpointItem");
        }
    }
}