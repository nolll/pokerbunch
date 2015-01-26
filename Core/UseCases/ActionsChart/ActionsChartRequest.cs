using System;

namespace Core.UseCases.ActionsChart
{
    public class ActionsChartRequest
    {
        public string Slug { get; private set; }
        public string DateStr { get; private set; }
        public int PlayerId { get; private set; }
        public DateTime CurrentTime { get; private set; }

        public ActionsChartRequest(string slug, string dateStr, int playerId, DateTime currentTime)
        {
            Slug = slug;
            DateStr = dateStr;
            PlayerId = playerId;
            CurrentTime = currentTime;
        }
    }
}