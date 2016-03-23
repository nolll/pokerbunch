namespace Core.UseCases
{
    public class BaseContext
    {
        public Result Execute()
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            return new Result(version);
        }

        public class Result
        {
            public string Version { get; }

            public Result(string version)
            {
                Version = version;
            }
        }
    }
}