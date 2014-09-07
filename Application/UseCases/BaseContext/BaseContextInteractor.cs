using Application.Services;

namespace Application.UseCases.BaseContext
{
    public class BaseContextInteractor
    {
        public static BaseContextResult Execute(IWebContext webContext)
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            return new BaseContextResult(
                IsInProduction(webContext),
                version);
        }

        private static bool IsInProduction(IWebContext webContext)
        {
            var host = webContext.Host;
            return host.Contains("pokerbunch.com");
        }
    }
}