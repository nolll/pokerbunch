namespace PokerBunch.Common.Urls
{
    public abstract class Url
    {
        protected abstract string Input { get; }
        public string Relative => Input != null ? string.Concat((string) "/", (string) Input).ToLower() : string.Empty;
        public string Absolute(string host, int port = 80, string protocol = "http")
        {
            var hostAndPort = port == 80 ? host : $"{host}:{port}";
            return $"{protocol}://{hostAndPort}{Relative}";
        }
    }
}
