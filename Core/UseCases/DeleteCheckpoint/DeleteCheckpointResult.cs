using Core.Urls;

namespace Core.UseCases.DeleteCheckpoint
{
    public class DeleteCheckpointResult
    {
        public Url ReturnUrl { get; private set; }

        public DeleteCheckpointResult(Url returnUrl)
        {
            ReturnUrl = returnUrl;
        }
    }
}