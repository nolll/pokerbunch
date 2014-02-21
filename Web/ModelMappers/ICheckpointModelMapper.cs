using System;
using Core.Classes.Checkpoints;
using Web.Models.CashgameModels.Buyin;
using Web.Models.CashgameModels.Cashout;
using Web.Models.CashgameModels.Checkpoints;
using Web.Models.CashgameModels.Report;

namespace Web.ModelMappers
{
    public interface ICheckpointModelMapper
    {
        Checkpoint GetCheckpoint(CashoutPostModel postModel, Checkpoint existingCashoutCheckpoint);
        Checkpoint GetCheckpoint(ReportPostModel postModel);
        Checkpoint GetCheckpoint(BuyinPostModel postModel);
        Checkpoint GetCheckpoint(EditCheckpointPostModel postModel, Checkpoint existingCheckpoint, TimeZoneInfo timeZone);
    }
}