namespace Core.Classes
{
    public class MoneyWinRate : MoneyResult
    {
        public MoneyWinRate(int amount) : base(amount)
        {
        }

        public MoneyWinRate(int amount, Currency currency) : base(amount, currency)
        {
        }

        public override string ToString()
        {
            return base.ToString() + "/h";
        }
    }
}