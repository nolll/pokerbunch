using System;
using Core.Entities.Checkpoints;
using Web.Models.CashgameModels.Cashout;
using Web.Models.CashgameModels.Checkpoints;
using Web.Models.CashgameModels.Report;

namespace Web.ModelMappers
{
    public interface ICheckpointModelMapper
    {
        Checkpoint GetCheckpoint(CashoutPostModel postModel, Checkpoint existingCashoutCheckpoint);
        Checkpoint GetCheckpoint(ReportPostModel postModel);
        Checkpoint GetCheckpoint(EditCheckpointPostModel postModel, Checkpoint existingCheckpoint, TimeZoneInfo timeZone);
    }
}