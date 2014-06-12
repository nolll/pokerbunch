using System;
using Core.Entities;

namespace Tests.Common.FakeClasses
{
    public class HomegameInTest : Homegame
    {
        public HomegameInTest(
            int id = default(int),
            string slug = default(string),
            string displayName = default(string),
            string description = default(string),
            string houseRules = default(string),
            TimeZoneInfo timezone = default(TimeZoneInfo),
            int defaultBuyin = default(int),
            Currency currency = default(Currency)
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

        private static Currency DefaultCurrency
        {
            get
            {
                return new Currency("$", "{SYMBOL}{AMOUNT}");
            }
        }
    }
}