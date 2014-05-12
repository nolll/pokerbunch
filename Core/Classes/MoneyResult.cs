namespace Core.Classes
{
    public class MoneyResult : Money
    {
        public MoneyResult(int amount) : base(amount)
        {
        }

        public MoneyResult(int amount, Currency currency) : base(amount, currency)
        {
        }

        public override string ToString()
        {
            var str = base.ToString();
            if (Amount > 0)
            {
                return "+" + str;
            }
            return str;
        }
    }
}