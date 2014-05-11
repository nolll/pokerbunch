using System;

namespace Core.Classes
{
    public class Money : IComparable<Money>
    {
        public int Amount { get; private set; }
        public Currency Currency { get; private set; }

        public Money(int amount) : this(amount, Currency.Default)
        {
        }

        public Money(int amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public int CompareTo(Money other)
        {
            return Amount.CompareTo(other.Amount);
        }

        public override string ToString()
        {
            var numberFormatted = Amount.ToString("N0", Currency.Culture);
            var amountFormatted = Currency.Layout.Replace("{AMOUNT}", numberFormatted);
            return amountFormatted.Replace("{SYMBOL}", Currency.Symbol);
        }
    }
}