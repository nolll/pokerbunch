using JetBrains.Annotations;

namespace Infrastructure.Api.Models
{
    public class ApiCashgameBunch
    {
        [UsedImplicitly]
        public string Id { get; set; }
        [UsedImplicitly]
        public string Timezone { get; set; }
        [UsedImplicitly]
        public string CurrencySymbol { get; set; }
        [UsedImplicitly]
        public string CurrencyLayout { get; set; }
        [UsedImplicitly]
        public string ThousandSeparator { get; set; }
        [UsedImplicitly]
        public string Culture { get; set; }
        [UsedImplicitly]
        public string Role { get; set; }
    }
}