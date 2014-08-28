using System;
using Core.Entities;

namespace Tests.Common.FakeClasses
{
    public class BunchInTest : Bunch
    {
        public BunchInTest(
            int id = 0,
            string slug = "",
            string displayName = "",
            string description = "",
            string houseRules = "",
            TimeZoneInfo timezone = null,
            int defaultBuyin = 0,
            Currency currency = null
            ) : base(
                id,
                slug,
                displayName,
                description,
                houseRules,
                timezone ?? TimeZoneInfo.Utc,
                defaultBuyin,
                currency ?? new Currency("$", "{SYMBOL}{AMOUNT}"))
        {
        }
    }
}