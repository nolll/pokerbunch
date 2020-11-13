namespace PokerBunch.Common.Routes
{
    public static class ApiRoutes
    {
        public static class Bunch
        {
            public const string Get = "bunches/{bunchId}";
            public const string Join = "bunches/{bunchId}/join";
        }

        public static class Cashgame
        {
            public const string Get = "cashgames/{cashgameId}";
            public const string ListByPlayer = "players/{playerId}/cashgames";
        }
        
        public static class Event
        {
            public const string ListByBunch = "bunches/{bunchId}/events";
        }

        public static class Location
        {
            public const string Get = "locations/{locationId}";
            public const string ListByBunch = "bunches/{bunchId}/locations";
        }

        public static class Player
        {
            public const string Get = "players/{playerId}";
            public const string Invite = "players/{playerId}/invite";
        }

        public static class Profile
        {
            public const string Get = "user";
            public const string PasswordReset = "user/password/reset";
        }

        public static class Token
        {
            public const string Get = "token";
        }

        public static class User
        {
            public const string Get = "users/{userName}";
        }
    }
}