﻿namespace Core.Entities
{
    public class MoneyWinRate : Money
    {
        public MoneyWinRate(int amount) : base(amount)
        {
        }

        public MoneyWinRate(int amount, Currency currency) : base(amount, currency)
        {
        }

        public override string ToString()
        {
            return base.String + "/h";
        }

        public override string String => base.String + "/h";
    }
}