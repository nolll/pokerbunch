using Core.Entities;
using JetBrains.Annotations;

namespace Infrastructure.Api.Models
{
    internal class ApiBunch : ApiSmallBunch
    {
        [UsedImplicitly]
        public string HouseRules { get; set; }
        [UsedImplicitly]
        public string Timezone { get; set; }
        [UsedImplicitly]
        public string CurrencySymbol { get; set; }
        [UsedImplicitly]
        public string CurrencyLayout { get; set; }
        [UsedImplicitly]
        public int DefaultBuyin { get; set; }
        [UsedImplicitly]
        public ApiBunchPlayer Player { get; set; }
        [UsedImplicitly]
        public string Role { get; set; }

        public ApiBunch(Bunch b)
            : base(b)
        {
            HouseRules = b.HouseRules;
            Timezone = b.Timezone.Id;
            CurrencySymbol = b.Currency.Symbol;
            CurrencyLayout = b.Currency.Layout;
            DefaultBuyin = b.DefaultBuyin;
            Player = new ApiBunchPlayer();
            Role = b.Role.ToString().ToLower();
        }

        public ApiBunch()
        {
        }
    }
}