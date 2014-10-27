using Core.Services;

namespace Core.UseCases.BaseContext
{
    public class BaseContextInteractor
    {
        public static BaseContextResult Execute()
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            return new BaseContextResult(
                Env.IsInProduction,
                version);
        }
    }
}