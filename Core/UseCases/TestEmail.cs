using Core.Services;

namespace Core.UseCases
{
    public class TestEmail
    {
        private readonly IAdminService _adminService;

        public TestEmail(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public Result Execute()
        {
            var message = _adminService.SendEmail();

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