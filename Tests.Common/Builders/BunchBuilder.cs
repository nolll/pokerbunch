using System;
using Core.Entities;

namespace Tests.Common.Builders
{
    public class BunchBuilder
    {
        private int _id;
        private string _slug;
        private string _displayName;
        private string _description;
        private string _houseRules;
        private TimeZoneInfo _timeZone;
        private int _defaultBuyin;
        private Currency _currency;

        public BunchBuilder()
        {
            _id = 1;
            _slug = "";
            _displayName = "";
            _description = "";
            _houseRules = "";
            _timeZone = TimeZoneInfo.Utc;
            _defaultBuyin = 0;
            _currency = Currency.Default;

        }

        public Bunch Build()
        {
            return new Bunch(_id, _slug, _displayName, _description, _houseRules, _timeZone, _defaultBuyin, _currency);
        }

        public BunchBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public BunchBuilder WithSlug(string slug)
        {
            _slug = slug;
            return this;
        }

        public BunchBuilder WithDisplayName(string displayName)
        {
            _displayName = displayName;
            return this;
        }

        public BunchBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public BunchBuilder WithHouseRules(string houseRules)
        {
            _houseRules = houseRules;
            return this;
        }

        public BunchBuilder WithDefaultBuyin(int defaultBuyin)
        {
            _defaultBuyin = defaultBuyin;
            return this;
        }

        public BunchBuilder WithCurrency(Currency currency)
        {
            _currency = currency;
            return this;
        }

        public BunchBuilder WithTimeZone(TimeZoneInfo timeZone)
        {
            _timeZone = timeZone;
            return this;
        }

        public BunchBuilder WithUtcTimeZone()
        {
            _timeZone = TimeZoneInfo.Utc;
            return this;
        }

        public BunchBuilder WithLocalTimeZone()
        {
            _timeZone = TestService.LocalTimeZone;
            return this;
        }
    }
}