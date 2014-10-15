using Core.Urls;

namespace Core.UseCases.JoinBunchConfirmation
{
    public class JoinBunchConfirmationResult
    {
        public string BunchName { get; private set; }
        public Url BunchDetailsUrl { get; private set; }

        public JoinBunchConfirmationResult(string bunchName, Url bunchDetailsUrl)
        {
            BunchDetailsUrl = bunchDetailsUrl;
            BunchName = bunchName;
        }
    }
}