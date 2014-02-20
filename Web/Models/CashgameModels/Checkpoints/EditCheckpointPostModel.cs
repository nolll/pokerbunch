using System;

namespace Web.Models.CashgameModels.Checkpoints
{
    public class EditCheckpointPostModel
    {
        public DateTime Timestamp { get; set; }
        public int Stack { get; set; }
        public int Amount { get; set; }
    }
}