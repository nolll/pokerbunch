using Core.Services;
using Core.UseCases;
using Web.Common.Urls.SiteUrls;

namespace Web.Models.CashgameModels.Action
{
    public class CheckpointModel
    {
        public string Description { get; private set; }
        public string Stack { get; private set; }
        public string Timestamp { get; private set; }
        public bool ShowLink { get; private set; }
        public string EditUrl { get; private set; }

        public CheckpointModel(Actions.CheckpointItem checkpointItem)
        {
            Description = checkpointItem.Type;
            Stack = checkpointItem.DisplayAmount.String;
            Timestamp = Globalization.FormatTime(checkpointItem.Time);
            ShowLink = checkpointItem.CanEdit;
            EditUrl = new EditCheckpointUrl(checkpointItem.CheckpointId).Relative;
        }
    }
}