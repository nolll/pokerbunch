namespace Core.Entities
{
    public class MoneyResult : Money
    {
        public MoneyResult(int amount, Currency currency) : base(amount, currency)
        {
        }

        public override string String
        {
            get
            {
                var str = base.String;
                if (Amount > 0)
                    return "+" + str;
                return str;
            }
        }
    }
}