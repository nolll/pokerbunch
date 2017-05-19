using Plumbing;

namespace Web
{
    public class Bootstrapper
    {
        public UseCaseContainer UseCases { get; private set; }

        public Bootstrapper(string apiUrl, string apiKey, string apiToken, bool isDetailedErrorMessagesEnabled)
        {
            var deps = new Dependencies(apiUrl, apiKey, apiToken, isDetailedErrorMessagesEnabled);
            UseCases = new UseCaseContainer(deps);
        }
    }
}