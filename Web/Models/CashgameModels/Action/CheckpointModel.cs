using Application.Services;
using Application.UseCases.Actions;

namespace Web.Models.CashgameModels.Action
{
    public class CheckpointModel
    {
        public string Description { get; set; }
        public string Stack { get; set; }
        public string Timestamp { get; set; }
        public bool ShowLink { get; set; }
        public string EditUrl { get; set; }

        public CheckpointModel()
        {
        }

        public CheckpointModel(CheckpointItem checkpointItem)
        {
            Description = checkpointItem.Type;
            Stack = checkpointItem.Stack.ToString();
            Timestamp = Globalization.FormatTimeStatic(checkpointItem.Time);
            ShowLink = checkpointItem.CanEdit;
            EditUrl = checkpointItem.EditUrl.Relative;
        }
    }
}