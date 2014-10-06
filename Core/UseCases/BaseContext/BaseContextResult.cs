namespace Core.UseCases.BaseContext
{
    public class BaseContextResult
    {
        public bool IsInProduction { get; private set; }
        public string Version { get; private set; }

        public BaseContextResult(
            bool isInProduction,
            string version)
        {
            IsInProduction = isInProduction;
            Version = version;
        }
    }
}