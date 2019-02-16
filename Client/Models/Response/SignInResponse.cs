namespace PokerBunch.Client.Models.Response
{
    internal class SignInResponse
    {
        // ReSharper disable once InconsistentNaming
        public string access_token { get; set; }

        // ReSharper disable once InconsistentNaming
        public string token_type { get; set; }

        // ReSharper disable once InconsistentNaming
        public string expires_in { get; set; }
    }
}