using JetBrains.Annotations;

namespace Web.Models.CashgameModels.Add
{
	public class AddCashgamePostModel
    {
        public int LocationId { get; [UsedImplicitly] set; }
        public int EventId { get; [UsedImplicitly] set; }
	}
}