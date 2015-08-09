using Core.Services;

namespace Core.UseCases
{
    public class BaseContext
    {
        public Result Execute()
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            return new Result(
                Env.IsInProduction,
                version);
        }

        public class Result
        {
            public bool IsInProduction { get; private set; }
            public string Version { get; private set; }

            public Result(
                bool isInProduction,
                string version)
            {
                IsInProduction = isInProduction;
                Version = version;
            }
        }
    }
}