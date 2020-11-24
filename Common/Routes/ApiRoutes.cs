namespace PokerBunch.Common.Routes
{
    public static class ApiRoutes
    {
        public static class Bunch
        {
            public const string Get = "bunches/{bunchId}";
        }

        public static class Player
        {
            public const string Get = "players/{playerId}";
        }

        public static class Profile
        {
            public const string Get = "user";
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