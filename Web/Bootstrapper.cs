using Plumbing;

namespace Web
{
    public class Bootstrapper
    {
        public UseCaseContainer UseCases { get; private set; }

        public Bootstrapper(string apiUrl, string apiKey, string apiToken)
        {
            var deps = new Dependencies(apiUrl, apiKey, apiToken);
            UseCases = new UseCaseContainer(deps);
        }
    }
}