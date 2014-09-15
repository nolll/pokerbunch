namespace Application.Services
{
    public static class Env
    {
        public static bool IsInProduction(string host)
        {
            return host.Contains("pokerbunch.com");
        }
    }
}
