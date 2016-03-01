using JetBrains.Annotations;

namespace Web.Models.HomegameModels.Edit
{
    public class EditBunchPostModel
    {
        public string Description { get; [UsedImplicitly] set; }
        public string CurrencySymbol { get; [UsedImplicitly] set; }
        public string CurrencyLayout { get; [UsedImplicitly] set; }
        public string TimeZone { get; [UsedImplicitly] set; }
        public string HouseRules { get; [UsedImplicitly] set; }
        public int DefaultBuyin { get; [UsedImplicitly] set; }
        /*
        public bool CashgamesEnabled { get; [UsedImplicitly] set; }
        public bool TournamentsEnabled { get; [UsedImplicitly] set; }
        public bool VideosEnabled { get; [UsedImplicitly] set; }
        */
    }
}