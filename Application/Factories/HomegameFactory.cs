using System;
using Core.Entities;

namespace Application.Factories
{
    public class HomegameFactory : IHomegameFactory
    {
        public Homegame Create(
            int id,
            string slug,
            string displayName,
            string description,
            string houseRules,
            TimeZoneInfo timeZone,
            int defaultBuyin,
            Currency currency)
        {
            return new Homegame
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