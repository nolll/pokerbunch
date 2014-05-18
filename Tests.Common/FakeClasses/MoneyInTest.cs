using System.Globalization;
using Core.Entities;

namespace Tests.Common.FakeClasses
{
    public class MoneyInTest : Money
    {
        public MoneyInTest(int amount = default(int), Currency currency = null) : base(amount, currency)
        {
        }

        public override string ToString()
        {
            return Amount.ToString(CultureInfo.InvariantCulture);
        }
    }
}