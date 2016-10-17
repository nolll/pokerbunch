using System;

namespace Core.Entities
{
    public class Bunch : SmallBunch
    {
        public string HouseRules { get; }
        public TimeZoneInfo Timezone { get; }
        public int DefaultBuyin { get; }
        public Role Role { get; }
        public Currency Currency { get; }
        public bool CashgamesEnabled { get; }
        public bool TournamentsEnabled { get; }
        public bool VideosEnabled { get; }

        public Bunch(
            string id,
            string slug,
            string displayName = null,
            string description = null,
            string houseRules = null,
            TimeZoneInfo timezone = null,
            int defaultBuyin = 0,
            Currency currency = null,
            Role role = Role.None)
            : base(
                  id,
                  slug,
                  displayName,
                  description)
        {
            HouseRules = houseRules ?? string.Empty;
            Timezone = timezone ?? TimeZoneInfo.Utc;
            DefaultBuyin = defaultBuyin;
            Role = role;
            Currency = currency ?? Currency.Default;
            CashgamesEnabled = true;
            TournamentsEnabled = false;
            VideosEnabled = false;
        }
    }

    public class SmallBunch : IEntity
    {
	    public string Id { get; }
	    public string Slug { get; }
        public string DisplayName { get; }
        public string Description { get; }
        public string CacheId => Id;

        public SmallBunch(
            string id, 
            string slug, 
            string displayName = null,
            string description = null)
	    {
	        Id = id;
	        Slug = slug;
	        DisplayName = displayName ?? string.Empty;
            Description = description ?? string.Empty;
        }
	}
}
