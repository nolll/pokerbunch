using JetBrains.Annotations;

namespace Web.Models.HomegameModels.Add
{
    public class AddBunchPostModel
    {
        public string DisplayName { get; [UsedImplicitly] set; }
        public string Description { get; [UsedImplicitly] set; }
        public string CurrencySymbol { get; [UsedImplicitly] set; }
        public string CurrencyLayout { get; [UsedImplicitly] set; }
        public string TimeZone { get; [UsedImplicitly] set; }
    }
}