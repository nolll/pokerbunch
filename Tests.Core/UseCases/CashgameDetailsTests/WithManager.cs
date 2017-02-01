using Core.Entities;
using Core.UseCases;
using NUnit.Framework;

namespace Tests.Core.UseCases.CashgameDetailsTests
{
    public class WithManager : Arrange
    {
        protected override Role Role => Role.Manager;

        [Test]
        public void CanEditIsTrue()
        {
            var request = new CashgameDetails.Request(Id);

            var result = Execute(request);

            Assert.IsTrue(result.CanEdit);
        }
    }
}