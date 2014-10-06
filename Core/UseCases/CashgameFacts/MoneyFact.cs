using Core.Entities;

namespace Core.UseCases.CashgameFacts
{
    public class MoneyFact : PlayerFact
    {
        public Money Amount { get; private set; }

        public MoneyFact(string playerName, Money amount) : base(playerName)
        {
            Amount = amount;
        }
    }
}