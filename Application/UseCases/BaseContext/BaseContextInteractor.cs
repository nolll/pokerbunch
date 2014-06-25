using Application.UseCases.AppContext;

namespace Application.UseCases.BaseContext
{
    public class BaseContextInteractor : IBaseContextInteractor
    {
        public BaseContextResult Execute()
        {
            const bool isInProduction = false;
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            return new BaseContextResult(
                isInProduction,
                version);
        }
    }
}