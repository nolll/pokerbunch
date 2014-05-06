namespace Application.UseCases.CashgameFacts
{
    public class DurationFact : PlayerFact
    {
        public int Minutes { get; private set; }

        public DurationFact(string playerName, int minutes) : base(playerName)
        {
            Minutes = minutes;
        }
    }
}