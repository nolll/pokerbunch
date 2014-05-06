using Core.Classes;

namespace Application.UseCases.CashgameFacts
{
    public class AmountFact : PlayerFact
    {
        public Money Amount { get; private set; }

        public AmountFact(string playerName, Money amount) : base(playerName)
        {
            Amount = amount;
        }
    }
}