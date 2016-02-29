namespace Web.Common
{
    public static class Environment
    {
        public static bool IsDev(string hostName)
        {
            return hostName.EndsWith("pokerbunch.lan");
        }

        public static bool IsStage(string hostName)
        {
            return hostName.EndsWith("staging.pokerbunch.com");
        }
    }
}