using Application.Services;
using Application.UseCases.Actions;

namespace Web.Models.CashgameModels.Action
{
    public class CheckpointModel
    {
        public string Description { get; private set; }
        public string Stack { get; private set; }
        public string Timestamp { get; private set; }
        public bool ShowLink { get; private set; }
        public string EditUrl { get; private set; }

        public CheckpointModel(CheckpointItem checkpointItem)
        {
            Description = checkpointItem.Type;
            Stack = checkpointItem.Stack.ToString();
            Timestamp = Globalization.FormatTime(checkpointItem.Time);
            ShowLink = checkpointItem.CanEdit;
            EditUrl = checkpointItem.EditUrl.Relative;
        }
    }
}