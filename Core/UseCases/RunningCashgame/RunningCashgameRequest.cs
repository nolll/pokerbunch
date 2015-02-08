using System;

namespace Core.UseCases.RunningCashgame
{
    public class RunningCashgameRequest
    {
        public string Slug { get; private set; }
        public int UserId { get; private set; }
        public DateTime CurrentTime { get; private set; }

        public RunningCashgameRequest(string slug, int userId, DateTime currentTime)
        {
            Slug = slug;
            UserId = userId;
            CurrentTime = currentTime;
        }
    }
}