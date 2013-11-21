using System;
using Core.Classes;

namespace Tests.Common.FakeClasses
{
    public class FakeHomegame : Homegame
    {
        public FakeHomegame(
            int id = default(int),
            string slug = default(string),
            string displayName = default(string),
            string description = default(string),
            string houseRules = default(string),
            TimeZoneInfo timezone = default(TimeZoneInfo),
            int defaultBuyin = default(int),
            CurrencySettings currency = default(CurrencySettings)
            ) : base(
                id,
                slug,
                displayName,
                description,
                houseRules,
                timezone ?? DefaultTimezone,
                defaultBuyin,
                currency ?? DefaultCurrency)
        {
        }

        private static TimeZoneInfo DefaultTimezone
        {
            get
            {
                return TimeZoneInfo.Utc;
            }
        }

        private static CurrencySettings DefaultCurrency
        {
            get
            {
                return new CurrencySettings("$", "{SYMBOL}{AMOUNT}");
            }
        }
    }
}