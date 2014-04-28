using System;
using Core.Classes;

namespace Application.Factories
{
    public interface IHomegameFactory
    {
        Homegame Create(
            int id,
            string slug,
            string displayName,
            string description,
            string houseRules,
            TimeZoneInfo timeZone,
            int defaultBuyin,
            Currency currency);
    }
}