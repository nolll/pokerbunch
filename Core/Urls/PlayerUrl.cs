using System.Globalization;

namespace Core.Urls
{
    public abstract class PlayerUrl : Url
    {
        protected PlayerUrl(string format, string slug, int playerId)
            : base(format.Replace("{slug}", slug).Replace("{playerId}", playerId.ToString(CultureInfo.InvariantCulture)))
        {
        }

        protected PlayerUrl(string format, int playerId)
            : this(format, "-", playerId)
        {
        }
    }
}