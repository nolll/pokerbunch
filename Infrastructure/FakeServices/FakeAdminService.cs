using Core.Services;

namespace Infrastructure.Api.FakeServices
{
    public class FakeAdminService : IAdminService
    {
        public string ClearCache()
        {
            return "Fake cache cleared";
        }

        public string SendEmail()
        {
            return "Fake email sent";
        }
    }
}