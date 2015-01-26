using System;

namespace Core.UseCases.RunningCashgame
{
    public class RunningCashgameRequest
    {
        public string Slug { get; private set; }
        public DateTime CurrentTime { get; private set; }

        public RunningCashgameRequest(string slug, DateTime currentTime)
        {
            Slug = slug;
            CurrentTime = currentTime;
        }
    }
}