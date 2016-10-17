using JetBrains.Annotations;

namespace Web.Models.CashgameModels.Edit
{
	public class EditCashgamePostModel
    {
        public string LocationId { get; [UsedImplicitly] set; }
        public string EventId { get; [UsedImplicitly] set; }
    }
}