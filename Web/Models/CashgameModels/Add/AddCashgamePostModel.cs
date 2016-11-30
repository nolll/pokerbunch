using JetBrains.Annotations;

namespace Web.Models.CashgameModels.Add
{
	public class AddCashgamePostModel
    {
        public string LocationId { get; [UsedImplicitly] set; }
        public string EventId { get; [UsedImplicitly] set; }
	}
}