using System;
using Core.Classes.Checkpoints;
using Web.Models.CashgameModels.Buyin;
using Web.Models.CashgameModels.Cashout;
using Web.Models.CashgameModels.Report;

namespace Web.ModelMappers
{
    public interface ICheckpointModelMapper
    {
        Checkpoint GetCheckpoint(CashoutPostModel postModel, TimeZoneInfo timeZone);
        Checkpoint GetCheckpoint(ReportPostModel postModel, TimeZoneInfo timeZone);
        Checkpoint GetCheckpoint(BuyinPostModel postModel, TimeZoneInfo timeZone);
    }
}