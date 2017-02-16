namespace Infrastructure.Storage.Repositories
{
    public class ApiUrlBuilder
    {
        public ApiUrl AppSingle(string id) => new ApiUrl($"apps/{id}");
        public ApiUrl AppList => new ApiUrl("apps");
        public ApiUrl AppUserList => new ApiUrl("user/apps");

        public ApiUrl BunchSingle(string id) => new ApiUrl($"bunches/{id}");
        public ApiUrl BunchList => new ApiUrl("bunches");
        public ApiUrl BunchUserList => new ApiUrl("user/bunches");

        public ApiUrl LocationSingle(string id) => new ApiUrl($"locations/{id}");
        public ApiUrl LocationList => new ApiUrl("locations");
        public ApiUrl LocationBunchUserList(string id) => new ApiUrl($"bunches/{id}/locations");
    }
}