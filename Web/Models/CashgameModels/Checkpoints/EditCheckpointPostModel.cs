using System;
using JetBrains.Annotations;

namespace Web.Models.CashgameModels.Checkpoints
{
    public class EditCheckpointPostModel
    {
        public DateTime Timestamp { get; [UsedImplicitly] set; }
        public int Stack { get; [UsedImplicitly] set; }
        public int Amount { get; [UsedImplicitly] set; }
    }
}