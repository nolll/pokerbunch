using Core.Entities;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.Entities.MoneyTests
{
    public abstract class Arrange : ArrangeBase
    {
        protected virtual Currency Currency => Currency.Default;

        [SetUp]
        public void Setup()
        {
        }
    }
}
