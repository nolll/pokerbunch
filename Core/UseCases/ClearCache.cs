using Core.Services;

namespace Core.UseCases
{
    public class ClearCache
    {
        private readonly IAdminService _adminService;

        public ClearCache(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public Result Execute()
        {
            var message = _adminService.ClearCache();

            return new Result(message);
        }

        public class Result
        {
            public string Message { get; }

            public Result(string message)
            {
                Message = message;
            }
        }
    }
}
