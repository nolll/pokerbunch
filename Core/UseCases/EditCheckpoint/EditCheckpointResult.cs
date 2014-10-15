using Core.Urls;

namespace Core.UseCases.EditCheckpoint
{
    public class EditCheckpointResult
    {
        public Url ReturnUrl { get; private set; }

        public EditCheckpointResult(Url returnUrl)
        {
            ReturnUrl = returnUrl;
        }
    }
}