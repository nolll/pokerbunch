namespace Infrastructure.ApiUrls
{
    public class ApiUrlBuilder
    {
        public ApiUrl AppSingle(string id) => new SimpleApiUrl($"apps/{id}");
        public ApiUrl AppList => new SimpleApiUrl("apps");
        public ApiUrl AppUserList => new SimpleApiUrl("user/apps");

        public ApiUrl BunchSingle(string id) => new SimpleApiUrl($"bunches/{id}");
        public ApiUrl BunchList => new SimpleApiUrl("bunches");
        public ApiUrl BunchUserList => new SimpleApiUrl("user/bunches");

        public ApiUrl LocationSingle(string id) => new SimpleApiUrl($"locations/{id}");
        public ApiUrl LocationList => new SimpleApiUrl("locations");
        public ApiUrl LocationBunchList(string id) => new SimpleApiUrl($"bunches/{id}/locations");

        public ApiUrl CashgameSingle(string id) => new SimpleApiUrl($"cashgames/{id}");
        public ApiUrl CashgameBunchList(string bunchId, int? year) => new CashgameBunchListUrl(bunchId, year);
        public ApiUrl CashgameEventList(string eventId) => new SimpleApiUrl($"events/{eventId}/cashgames");
        public ApiUrl CashgamePlayerList(string playerId) => new SimpleApiUrl($"players/{playerId}/cashgames");

        public ApiUrl EventSingle(string id) => new SimpleApiUrl($"events/{id}");
        public ApiUrl EventBunchList(string bunchId) => new SimpleApiUrl($"bunches/{bunchId}/events");

        public ApiUrl PlayerSingle(string id) => new SimpleApiUrl($"players/{id}");

        private class CashgameBunchListUrl : ApiUrl
        {
            private readonly string _bunchId;
            private readonly int? _year;

            public CashgameBunchListUrl(string bunchId, int? year = null)
            {
                _bunchId = bunchId;
                _year = year;
            }

            public string Url => _year.HasValue ? $"bunches/{_bunchId}/cashgames/{_year}" : $"bunches/{_bunchId}/cashgames";
        }
    }
}