using Core.Entities;

namespace Application.UseCases.CashgameFacts
{
    public class DurationFact : PlayerFact
    {
        public Time Time { get; private set; }

        public DurationFact(string playerName, Time time) : base(playerName)
        {
            Time = time;
        }
    }
}