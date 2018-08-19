using System;

namespace Core.Entities
{
    public class Bunch : SmallBunch
    {
        public string HouseRules { get; }
        public TimeZoneInfo Timezone { get; }
        public int DefaultBuyin { get; }
        public Role Role { get; }
        public string PlayerId { get; }
        public Currency Currency { get; }
        public bool CashgamesEnabled { get; }
        public bool TournamentsEnabled { get; }
        public bool VideosEnabled { get; }

        public Bunch(
            string id,
            string displayName = null,
            string description = null,
            string houseRules = null,
            TimeZoneInfo timezone = null,
            int defaultBuyin = 0,
            Currency currency = null,
            Role role = Role.None,
            string playerId = null)
            : base(
                  id,
                  displayName,
                  description)
        {
            HouseRules = houseRules ?? string.Empty;
            Timezone = timezone ?? TimeZoneInfo.Utc;
            DefaultBuyin = defaultBuyin;
            Role = role;
            PlayerId = playerId;
            Currency = currency ?? Currency.Default;
            CashgamesEnabled = true;
            TournamentsEnabled = false;
            VideosEnabled = false;
        }
    }
}
