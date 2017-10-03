using JetBrains.Annotations;

namespace Infrastructure.Api.Models.Response
{
    public class SignInResponse
    {
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        public string access_token { get; set; }

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        public string token_type { get; set; }

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        public string expires_in { get; set; }
    }
}