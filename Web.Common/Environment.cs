namespace Web.Common
{
    public static class Environment
    {
        public static bool IsDev(string hostName)
        {
            return hostName.EndsWith("pokerbunch.lan");
        }
    }
}