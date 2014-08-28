using System;
using Core.Entities;

namespace Application.Factories
{
    public static class BunchFactory
    {
        public static Bunch Create(
            int id,
            string slug,
            string displayName,
            string description,
            string houseRules,
            TimeZoneInfo timeZone,
            int defaultBuyin,
            Currency currency)
        {
            return new Bunch
                (
                    id,
                    slug,
                    displayName,
                    description,
                    houseRules,
                    timeZone,
                    defaultBuyin,
                    currency
                );
        }
    }
}