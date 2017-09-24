using Core.Services;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Extensions;

namespace Web.Models.CashgameModels.Action
{
    public class CheckpointModel : IViewModel
    {
        public string Description { get; }
        public string Stack { get; }
        public string Timestamp { get; }
        public bool ShowLink { get; }
        public string EditUrl { get; }

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