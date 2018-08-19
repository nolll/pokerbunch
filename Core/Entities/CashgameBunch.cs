using System;

namespace Core.Entities
{
    public class CashgameBunch
    {
        public string Id { get; }
        public TimeZoneInfo Timezone { get; }
        public Currency Currency { get; }

        public CashgameBunch(string id, TimeZoneInfo timezone, Currency currency)
        {
            Id = id;
            Timezone = timezone;
            Currency = currency;
        }
    }
}