namespace Core.UseCases
{
    public class BaseContext
    {
        public Result Execute(Request request)
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            return new Result(request.IsInProduction, version);
        }

        public class Request
        {
            public bool IsInProduction { get; }

            public Request(bool isInProduction)
            {
                IsInProduction = isInProduction;
            }
        }

        public class Result
        {
            public bool IsInProduction { get; }
            public string Version { get; }

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