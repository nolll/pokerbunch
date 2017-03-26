using Infrastructure.ApiUrls;

namespace Infrastructure.Storage.Services
{
    public abstract class BaseService
    {
        protected readonly ApiUrlBuilder Url;

        protected BaseService()
        {
            Url = new ApiUrlBuilder();
        }
    }
}