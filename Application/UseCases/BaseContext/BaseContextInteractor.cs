using Application.Services;

namespace Application.UseCases.BaseContext
{
    public class BaseContextInteractor : IBaseContextInteractor
    {
        private readonly IWebContext _webContext;

        public BaseContextInteractor(IWebContext webContext)
        {
            _webContext = webContext;
        }

        public BaseContextResult Execute()
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            return new BaseContextResult(
                IsInProduction,
                version);
        }

        private bool IsInProduction
        {
            get
            {
                var host = _webContext.Host;
                return host.Contains("pokerbunch.com");
            }
        }
    }
}