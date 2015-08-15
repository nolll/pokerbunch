using System.Globalization;

namespace Core.Urls
{
    public abstract class CheckpointUrl : Url
    {
        protected CheckpointUrl(string format, string slug, string dateStr, int playerId, int checkpointId)
            : base(format.Replace("{slug}", slug).Replace("{dateStr}", dateStr).Replace("{playerId}", playerId.ToString(CultureInfo.InvariantCulture)).Replace("{checkpointId}", checkpointId.ToString(CultureInfo.InvariantCulture)))
        {
        }
    }
}