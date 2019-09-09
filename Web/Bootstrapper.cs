using Plumbing;

namespace Web
{
    public class Bootstrapper
    {
        public UseCaseContainer UseCases { get; }

        public Bootstrapper(string apiHost, int apiPort, string apiProtocol, string apiKey, string apiToken, bool isDetailedErrorMessagesEnabled, bool useFakeData)
        {
            var deps = new Dependencies(apiHost, apiPort, apiProtocol, apiKey, apiToken, isDetailedErrorMessagesEnabled, useFakeData);
            UseCases = new UseCaseContainer(deps);
        }
    }
}