using Core.Services;
using Core.Services.Interfaces;

namespace Core.UseCases.BaseContext
{
    public class BaseContextInteractor
    {
        public static BaseContextResult Execute(IWebContext webContext)
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            return new BaseContextResult(
                Env.IsInProduction(webContext.Host),
                version);
        }
    }
}