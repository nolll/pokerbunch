using System;

namespace Core.Entities
{
    public class Money : IComparable<Money>
    {
        public int Amount { get; private set; }
        private readonly Currency _currency;
        
        public Money(int amount, Currency currency = null)
        {
            Amount = amount;
            _currency = currency ?? Currency.Default;
        }

        public int CompareTo(Money other)
        {
            return Amount.CompareTo(other.Amount);
        }

        public string String
        {
            get
            {
                var numberFormatted = Amount.ToString("N0", _currency.Culture);
                var amountFormatted = _currency.Layout.Replace("{AMOUNT}", numberFormatted);
                return amountFormatted.Replace("{SYMBOL}", _currency.Symbol);
            }
        }
    }
}