using System;

namespace Core.UseCases.CashgameDetailsChart
{
    public class CashgameDetailsChartRequest
    {
        public string Slug { get; private set; }
        public DateTime CurrentTime { get; private set; }
        public string DateStr { get; private set; }

        public CashgameDetailsChartRequest(string slug, DateTime currentTime, string dateStr = null)
        {
            Slug = slug;
            CurrentTime = currentTime;
            DateStr = dateStr;
        }
    }
}