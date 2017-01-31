using Core.Entities;
using Core.UseCases;
using NUnit.Framework;

namespace Tests.Core.UseCases.CashgameDetailsTests
{
    public class WithManager : Arrange
    {
        protected override Role Role => Role.Manager;

        [Test]
        public void CashgameDetails_WithManager_CanEditIsTrue()
        {
            var request = new CashgameDetails.Request(Id);

            var result = Execute(request);

            Assert.IsTrue(result.CanEdit);
        }
    }
}