using Core.Services;

namespace Core.UseCases.BaseContext
{
    public class BaseContextInteractor
    {
        public BaseContextResult Execute()
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            return new BaseContextResult(
                Env.IsInProduction,
                version);
        }
    }
}