using Infrastructure.ApiUrls;

namespace Infrastructure.Storage.Repositories
{
    public abstract class ApiRepository
    {
        protected readonly ApiUrlBuilder Url;

        protected ApiRepository()
        {
            Url = new ApiUrlBuilder();
        }
    }
}