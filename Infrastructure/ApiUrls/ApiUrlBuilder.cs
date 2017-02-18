namespace Infrastructure.ApiUrls
{
    public class ApiUrlBuilder
    {
        public ApiUrl App(string id) => new SimpleApiUrl($"apps/{id}");
        public ApiUrl Apps => new SimpleApiUrl("apps");
        public ApiUrl AppsByUser => new SimpleApiUrl("user/apps");

        public ApiUrl Bunch(string id) => new SimpleApiUrl($"bunches/{id}");
        public ApiUrl Bunches => new SimpleApiUrl("bunches");
        public ApiUrl BunchesByUser => new SimpleApiUrl("user/bunches");

        public ApiUrl Location(string id) => new SimpleApiUrl($"locations/{id}");
        public ApiUrl Locations => new SimpleApiUrl("locations");
        public ApiUrl LocationByBunch(string id) => new SimpleApiUrl($"bunches/{id}/locations");

        public ApiUrl Cashgame(string id) => new SimpleApiUrl($"cashgames/{id}");
        public ApiUrl CashgamesByBunch(string bunchId, int? year) => new CashgamesByBunchUrl(bunchId, year);
        public ApiUrl CashgamesByEvent(string eventId) => new SimpleApiUrl($"events/{eventId}/cashgames");
        public ApiUrl CashgamesByPlayer(string playerId) => new SimpleApiUrl($"players/{playerId}/cashgames");

        public ApiUrl Event(string id) => new SimpleApiUrl($"events/{id}");
        public ApiUrl EventsByBunch(string bunchId) => new SimpleApiUrl($"bunches/{bunchId}/events");

        public ApiUrl Player(string id) => new SimpleApiUrl($"players/{id}");

        private class CashgamesByBunchUrl : ApiUrl
        {
            private readonly string _bunchId;
            private readonly int? _year;

            public CashgamesByBunchUrl(string bunchId, int? year = null)
            {
                _bunchId = bunchId;
                _year = year;
            }

            public string Url => _year.HasValue ? $"bunches/{_bunchId}/cashgames/{_year}" : $"bunches/{_bunchId}/cashgames";
        }
    }
}