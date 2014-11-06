namespace Core.Entities
{
    public class MoneyResult : Money
    {
        public MoneyResult(int amount, Currency currency) : base(amount, currency)
        {
        }

        public string String
        {
            get
            {
                var str = base.ToString();
                if (Amount > 0)
                    return "+" + str;
                return str;
            }
        }
    }
}