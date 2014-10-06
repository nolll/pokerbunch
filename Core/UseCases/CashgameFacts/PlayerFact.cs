namespace Core.UseCases.CashgameFacts
{
    public class PlayerFact
    {
        public string PlayerName { get; private set; }

        protected PlayerFact(string playerName)
        {
            PlayerName = playerName;
        }
    }
}