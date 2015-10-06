using Web.Annotations;

namespace Web.Models.CashgameModels.Edit
{
	public class EditCashgamePostModel
    {
        public int LocationId { get; [UsedImplicitly] set; }
        public int EventId { get; [UsedImplicitly] set; }
    }
}