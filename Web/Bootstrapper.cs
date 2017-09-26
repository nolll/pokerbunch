using Plumbing;

namespace Web
{
    public class Bootstrapper
    {
        public UseCaseContainer UseCases { get; }

        public Bootstrapper(string apiHost, string apiProtocol, string apiKey, string apiToken, bool isDetailedErrorMessagesEnabled)
        {
            var deps = new Dependencies(apiHost, apiProtocol, apiKey, apiToken, isDetailedErrorMessagesEnabled);
            UseCases = new UseCaseContainer(deps);
        }
    }
}