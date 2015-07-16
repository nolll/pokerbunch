using System;

namespace Core.UseCases.RunningCashgame
{
    public class RunningCashgameRequest
    {
        public string Slug { get; private set; }
        public string UserName { get; private set; }
        public DateTime CurrentTime { get; private set; }

        public RunningCashgameRequest(string slug, string userName, DateTime currentTime)
        {
            Slug = slug;
            UserName = userName;
            CurrentTime = currentTime;
        }
    }
}